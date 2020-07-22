using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Blog.Core.AOP;
using Blog.Core.AuthHelper;
using Blog.Core.AuthHelper.OverWrite;
using Blog.Core.Common;
using Blog.Core.Common.MemoryCache;
using Blog.Core.FrameWork.IRepository;
using Blog.Core.Hubs;
using Blog.Core.IRepository;
using Blog.Core.IServices;
using Blog.Core.Model;
using Blog.Core.Log;
using Blog.Core.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using static Blog.Core.SwaggerHelper.CustomApiVersion;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using AutoMapper;
using log4net.Repository;
using log4net;
using log4net.Config;
using Blog.Core.Filter;
using Blog.Core.Log;
using StackExchange.Profiling.Storage;
using Blog.Core.Common.DB;

namespace Blog.Core
{
    public class Startup
    {
        /// <summary>
        /// log4net 仓储库
        /// </summary>
        public static ILoggerRepository repository { get; set; }
        private static readonly ILog log = LogManager.GetLogger(typeof(Startup));
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Env = env;
            //log4net
            repository = LogManager.CreateRepository("Blog.Core");//需要获取日志的仓库名，也就是你的当然项目名

            //指定配置文件，如果这里你遇到问题，应该是使用了InProcess模式，请查看Blog.Core.csproj,并删之 
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));//配置文件
        }
        public IHostingEnvironment Env { get; }
        private const string ApiName = "App.Core";
        public IConfiguration Configuration { get; }
 
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            #region 部分服务注入-netcore自带方法
            // 缓存注入
            services.AddScoped<ICaching, MemoryCaching>();
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });
            // Redis注入
            services.AddSingleton<IRedisCacheManager, RedisCacheManager>();//这里说下，如果是自己的项目，个人更建议使用单例模式 
            // log日志注入
            services.AddSingleton<ILoggerHelper, LogHelper>();
            #endregion
            #region  依赖注入 ISqlSugarClient
            // 这里我不是引用了命名空间，因为如果引用命名空间的话，会和Microsoft的一个GetTypeInfo存在二义性，所以就直接这么使用了。
            services.AddScoped<SqlSugar.ISqlSugarClient>(o =>
            {
                return new SqlSugar.SqlSugarClient(new SqlSugar.ConnectionConfig()
                {
                    ConnectionString = BaseDBConfig.ConnectionString,//必填, 数据库连接字符串
                    DbType = (SqlSugar.DbType)BaseDBConfig.DbType,//必填, 数据库类型
                    IsAutoCloseConnection = true,//默认false, 时候知道关闭数据库连接, 设置为true无需使用using或者Close操作
                    InitKeyType = SqlSugar.InitKeyType.SystemTable//默认SystemTable, 字段信息读取, 如：该属性是不是主键，标识列等等信息
                });
            });
            #endregion
            #region 初始化DB
            //services.AddSingleton<Love>();
            //反向自动生成数据库--不需要注释掉
            services.AddScoped<Blog.Core.Model.See.DBSeed>();
            services.AddScoped<Blog.Core.Model.See.MyContext>();
            #endregion

            #region Automapper
            services.AddAutoMapper(typeof(Startup));
            #endregion

            #region Swagger
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            services.AddSwaggerGen(c =>
            {
                //遍历出全部的版本，做文档信息展示
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                        c.SwaggerDoc(version, new Info
                    {
                        Version = version,
                        Title = $"{ApiName} 接口文档",
                        Description = $"{ApiName} HTTP API " ,
                        TermsOfService = "None",
                        Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "Blog.Core", Email = "Blog.Core@xxx.com", Url = "http://www.baidu.com" }
                    });//就是这里
                       // 按相对路径排序，作者：Alby
                    c.OrderActionsBy(o => o.RelativePath);
                });
               
                var xmlPath = Path.Combine(basePath, "Blog.Core.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改


                var xmlModelPath = Path.Combine(basePath, "Blog.Core.Model.xml");//这个就是Model层的xml文件名
                c.IncludeXmlComments(xmlModelPath, true);

                #region Token绑定到ConfigureServices
                //添加header验证信息
                //c.OperationFilter<SwaggerHeader>();
                var security = new Dictionary<string, IEnumerable<string>> { { "Blog.Core", new string[] { } }, };
                c.AddSecurityRequirement(security);
                //方案名称“Blog.Core”可自定义，上下一致即可
                c.AddSecurityDefinition("Blog.Core", new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });
             
            });
        
            #endregion
            #region Token服务注册
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });

            #region 参数
            //读取配置文件
            var audienceConfig = Configuration.GetSection("Audience");
            var symmetricKeyAsBase64 = audienceConfig["Secret"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            // 令牌验证参数
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Issuer"],//发行人
                ValidateAudience = true,
                ValidAudience = audienceConfig["Audience"],//订阅人
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
            };

            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // 如果要数据库动态绑定，这里先留个空，后边处理器里动态赋值
            var permission = new List<PermissionItem>();

            // 角色与接口的权限要求参数
            var permissionRequirement = new PermissionRequirement(
                "/api/denied",// 拒绝授权的跳转地址（目前无用）
                permission,
                ClaimTypes.Role,//基于角色的授权
                audienceConfig["Issuer"],//发行人
                audienceConfig["Audience"],//听众
                signingCredentials,//签名凭据
                expiration: TimeSpan.FromSeconds(60 * 60)//接口的过期时间
                );
            #endregion

            #region 【第一步：授权】

            #region 1、基于角色的API授权 

            // 1【授权】、这个很简单，其他什么都不用做， 只需要在API层的controller上边，增加特性即可，注意，只能是角色的:
            // [Authorize(Roles = "Admin,System")]


            #endregion
            #region 2、基于策略的授权（简单版和复杂版） 
                services.AddAuthorization(options =>
                {
                    //options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
                    //options.AddPolicy("Client1", policy => policy.RequireRole("Client1").Build());
                    //options.AddPolicy("Client2", policy => policy.RequireRole("Client2").Build());
                    options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
                    //这个写法是错误的，这个是并列的关系，不是或的关系
                    //options.AddPolicy("AdminOrClient", policy => policy.RequireRole("Admin,Client").Build());

                    //这个才是或的关系
                    options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));
                    options.AddPolicy("SystemOrAdminOrOther", policy => policy.RequireRole("Admin", "System", "Other"));
                    options.AddPolicy(Permissions.Name,
                             policy => policy.Requirements.Add(permissionRequirement));
                })// ② 核心之二，必需要配置认证服务，这里是jwtBearer默认认证，比如光有卡没用，得能识别他们
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }) // ③ 核心之三，针对JWT的配置，比如门禁是如何识别的，是放射卡，还是磁卡
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = tokenValidationParameters;
            });
            #endregion

            #endregion
            //认证
            #region  自定义的认证,自定义认证不够完善，建议使用官方的认证
            //     services.AddAuthentication(x =>
            //     {
            //         x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //         x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //     })
            //.AddJwtBearer(o =>
            //{
            //    o.TokenValidationParameters = new TokenValidationParameters
            //    {

            //        ValidateIssuer = true,//是否验证Issuer
            //           ValidateAudience = true,//是否验证Audience 
            //           ValidateIssuerSigningKey = true,//是否验证IssuerSigningKey 
            //           ValidIssuer = audienceConfig["Issuer"],//发行人
            //           ValidAudience = audienceConfig["Audience"],//订阅人
            //           ValidateLifetime = true,//是否验证超时  当设置exp和nbf时有效 同时启用ClockSkew 
            //           IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtHelper.secretKey)),
            //           //注意这是缓冲过期时间，总的有效时间等于这个时间加上jwt的过期时间
            //           ClockSkew = TimeSpan.Zero,
            //        RequireExpirationTime = true

            //    };
            //});
            #endregion

            #region 官方认证  暂时注释掉，如果不使用自定义设计可以加上，但不支持定义多个策略
           //这个方案上面已经自定义了，这不需要重复定义，重复定义会报错
          //  services.AddAuthentication("Bearer")
          //// 添加JwtBearer服务
          //.AddJwtBearer(o =>
          //{
          //    o.TokenValidationParameters = tokenValidationParameters;
          //    o.Events = new JwtBearerEvents
          //    {
          //        OnAuthenticationFailed = context =>
          //        {
          //               // 如果过期，则把<是否过期>添加到，返回头信息中
          //               if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
          //            {
          //                context.Response.Headers.Add("Token-Expired", "true");
          //            }
          //            return Task.CompletedTask;
          //        }
          //    };
          //});
            //2.2【认证】、IdentityServer4 认证 (暂时忽略)
            //services.AddAuthentication("Bearer")
            //  .AddIdentityServerAuthentication(options =>
            //  {
            //      options.Authority = "http://localhost:5002";
            //      options.RequireHttpsMetadata = false;
            //      options.ApiName = "blog.core.api";
            //  });
            // 注入权限处理器
            // 依赖注入，将自定义的授权处理器 匹配给官方授权处理器接口，这样当系统处理授权的时候，就会直接访问我们自定义的授权处理器了。
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            // 将授权必要类注入生命周期内
            services.AddSingleton(permissionRequirement);
            #endregion
            #endregion
            #endregion

            #region MiniProfiler性能监听
            services.AddMiniProfiler(options =>
            {
                options.RouteBasePath = "/profiler";//注意这个路径要和下边 index.html 脚本配置中的一致，
                (options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(10);

            });
            #endregion
            #region SignalR 通讯
            services.AddSignalR();
            #endregion

            #region CORS 跨域
            //跨域第一种方法，先注入服务，声明策略，然后再下边app中配置开启中间件
            services.AddCors(c =>
            {
                //↓↓↓↓↓↓↓注意正式环境不要使用这种全开放的处理↓↓↓↓↓↓↓↓↓↓
                c.AddPolicy("AllRequests", policy =>
                {
                    policy
                    .AllowAnyOrigin()//允许任何源
                    .AllowAnyMethod()//允许任何方式
                    .AllowAnyHeader()//允许任何头
                    .AllowCredentials();//允许cookie
                });
                //↑↑↑↑↑↑↑注意正式环境不要使用这种全开放的处理↑↑↑↑↑↑↑↑↑↑


                //一般采用这种方法
                c.AddPolicy("LimitRequests", policy =>
                {
                    policy
                    .WithOrigins("http://127.0.0.1:5000", "http://localhost:8080", "http://localhost:8021", "http://localhost:8081", "http://localhost:5000", "http://localhost:20101", "http://127.0.0.1:20101")//支持多个域名端口，注意端口号后不要带/斜杆：比如localhost:8000/，是错的
                    .AllowAnyHeader()//Ensures that the policy allows any header.
                    .AllowAnyMethod();
                    //.AllowCredentials();
                });
            });
            // 这是第二种注入跨域服务的方法，这里有歧义，部分读者可能没看懂，请看下边解释
            //services.AddCors();
            //跨域第一种版本，请要ConfigureService中配置服务 services.AddCors();
            //    app.UseCors(options => options.WithOrigins("http://localhost:8021").AllowAnyHeader()
            //.AllowAnyMethod());  
            #endregion

            services.AddMvc(o =>
            {
                o.Filters.Add(typeof(GlobalExceptionsFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton(new Appsettings(Env.ContentRootPath));
            #region AutoFac

            //实例化 AutoFac  容器   
            var builder = new ContainerBuilder();

            //注册要通过反射创建的组件
            //builder.RegisterType<AdvertisementServices>().As<IAdvertisementServices>();

            //var assemblysServices = Assembly.Load("Blog.Core.Services");//要记得!!!这个注入的是实现类层，不是接口层！不是 IServices
            //builder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces();//指定已扫描程序集中的类型注册为提供所有其实现的接口。
            //var assemblysRepository = Assembly.Load("Blog.Core.Repository");//模式是 Load(解决方案名)
            //builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces();
            #region 带有接口层的服务注入

            #region Service.dll 注入，有对应接口
            try
            {
                //注册要通过反射创建的组件
                //builder.RegisterType<BlogCacheAOP>();//可以直接替换其他拦截器
                //builder.RegisterType<BlogRedisCacheAOP>();//可以直接替换其他拦截器
                //builder.RegisterType<BlogLogAOP>();//可以直接替换其他拦截器！



                //var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;//获取项目路径
                //var servicesDllFile = Path.Combine(basePath, "Blog.Core.Services.dll");
                //var assemblysServices = Assembly.LoadFrom(servicesDllFile);//直接采用加载文件的方法  ※※★※※ 如果你是第一次下载项目，请先F6编译，然后再F5执行，※※★※※

                var servicesDllFile = Path.Combine(basePath, "Blog.Core.Services.dll");
                var assemblysServices = Assembly.LoadFrom(servicesDllFile);
                //builder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces();

                // AOP 开关，如果想要打开指定的功能，只需要在 appsettigns.json 对应对应 true 就行。
                var cacheType = new List<Type>();
                if (Appsettings.app(new string[] { "AppSettings", "RedisCachingAOP", "Enabled" }).ObjToBool())
                {
                    builder.RegisterType<BlogRedisCacheAOP>();//可以直接替换其他拦截器
                    cacheType.Add(typeof(BlogRedisCacheAOP));
                }
                if (Appsettings.app(new string[] { "AppSettings", "MemoryCachingAOP", "Enabled" }).ObjToBool())
                {
                    builder.RegisterType<BlogCacheAOP>();//可以直接替换其他拦截器
                    cacheType.Add(typeof(BlogCacheAOP));
                }
                if (Appsettings.app(new string[] { "AppSettings", "LogAOP", "Enabled" }).ObjToBool())
                {
                    builder.RegisterType<BlogLogAOP>();//可以直接替换其他拦截器！
                    cacheType.Add(typeof(BlogLogAOP));
                }
                if (Appsettings.app(new string[] { "AppSettings", "TranAOP", "Enabled" }).ObjToBool())
                {
                    builder.RegisterType<BlogTranAOP>();
                    cacheType.Add(typeof(BlogTranAOP));
                }
                builder.RegisterAssemblyTypes(assemblysServices)
                        .AsImplementedInterfaces()
                        .InstancePerLifetimeScope()
                        .EnableInterfaceInterceptors()//引用Autofac.Extras.DynamicProxy;
                        .InterceptedBy(cacheType.ToArray());//可以直接替换拦截器


                var repositoryDllFile = Path.Combine(basePath, "Blog.Core.Repository.dll");
                var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
                builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces()
                    .InstancePerDependency(); 

                #region 没有接口的单独类 class 注入
                ////只能注入该类中的虚方法
                //builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(Love)))
                //    .EnableClassInterceptors()
                //    .InterceptedBy(typeof(BlogLogAOP));

                #endregion

            }
            catch (Exception ex)
            {
                throw new Exception("※※★※※ 如果你是第一次下载项目，请先对整个解决方案dotnet build（F6编译），然后再对api层 dotnet run（F5执行），\n因为解耦了，如果你是发布的模式，请检查bin文件夹是否存在Repository.dll和service.dll ※※★※※" + ex.Message + "\n" + ex.InnerException);
            }

            #endregion


            #endregion


            //将services填充到Autofac容器生成器中
            builder.Populate(services);

            //使用已进行的组件登记创建新容器
            var ApplicationContainer = builder.Build();
            //ApplicationContainer.Resolve<Love>().SayLoveU();
            #endregion

            return new AutofacServiceProvider(ApplicationContainer);//第三方IOC接管 core内置DI容器
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{ApiName} V1");
                //1.遍历出全部的版本,依赖于上面的配置信息,根据上面定义的版本号名称来路由跳转     ---web API 增加版本控制  第一步
                //ConfigureServices  在Swagger服务根据接口版本来创建访问路径文件
                typeof(ApiVersions).GetEnumNames().OrderByDescending(e => e).ToList().ForEach(version =>
                {
                    c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{ApiName} {version}");
                });
                //c.RoutePrefix = "";//路径配置，设置为空，表示直接访问该文件，
                                   //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，
                                   //这个时候去launchSettings.json中把"launchUrl": "swagger/index.html"去掉， 然后直接访问localhost:8001/index.html即可

                                   // 将swagger首页，设置成我们自定义的页面，记得这个字符串的写法：解决方案名.index.html
                c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Blog.Core.index.html");

                if (GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Blog.Core.index.html") == null)
                {
                    var msg = "index.html的属性，必须设置为嵌入的资源";
                    log.Error(msg);
                    throw new Exception(msg);
                }

                // 路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，去launchSettings.json把launchUrl去掉，如果你想换一个路径，直接写名字即可，比如直接写c.RoutePrefix = "doc";
                c.RoutePrefix = "";
            });
            //将TokenAuth注册中间件
            //app.UseMiddleware<JwtTokenAuth>();//注意此授权方法已经放弃，请使用下边的官方验证方法。但是如果你还想传User的全局变量，还是可以继续使用中间件
            app.UseAuthentication();
            #endregion
            #region MiniProfiler性能监听
            app.UseMiniProfiler();
            #endregion
            #region CORS
            //跨域第二种方法，使用策略，详细策略信息在ConfigureService中
            app.UseCors("AllRequests");//将 CORS 中间件添加到 web 应用程序管线中, 以允许跨域请求。

            //app.UseExceptionHandler(options =>
            //{
            //    options.Run(
            //    async context =>
            //    {
            //        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //        context.Response.ContentType = "application/json;charset=utf-8"; //此处要加上utf-8编码

            //            //如果不加此句，服务器返回的数据到浏览器会拒绝
            //            context.Response.Headers["Access-Control-Allow-Origin"] = "*";

            //        var ex = context.Features.Get<IExceptionHandlerFeature>();
            //        if (ex != null)
            //        {
            //            var errObj = new
            //            {
            //                message = ex.Error.Message,
            //                stackTrace = ex.Error.StackTrace,
            //                exceptionType = ex.Error.GetType().Name
            //            };// $"<h1>Error: {ex.Error.Message}</h1>{ex.Error.StackTrace }";

            //                await context.Response.WriteAsync(JsonConvert.SerializeObject(errObj)).ConfigureAwait(false);
            //        }
            //    });
            //});


            #region 跨域第一种版本
            //跨域第一种版本，请要ConfigureService中配置服务 services.AddCors();
            //    app.UseCors(options => options.WithOrigins("http://localhost:8021").AllowAnyHeader()
            //.AllowAnyMethod());  
            #endregion

            #endregion

            // 跳转https
            //app.UseHttpsRedirection();
            // 使用静态文件
            app.UseStaticFiles();
            // 使用cookie
            app.UseCookiePolicy();
            // 返回错误码
            app.UseStatusCodePages();//把错误码返回前台，比如是404


            app.UseMvc();
            app.UseSignalR(routes =>
            {
                //这里要说下，为啥地址要写 /api/xxx 
                //因为我前后端分离了，而且使用的是代理模式，所以如果你不用/api/xxx的这个规则的话，会出现跨域问题，毕竟这个不是我的controller的路由，而且自己定义的路由
                routes.MapHub<ChatHub>("/api2/chatHub");
            });
        }
    }
}
