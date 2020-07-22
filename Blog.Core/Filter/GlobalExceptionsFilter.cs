using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Common.LogHelper;
using Blog.Core.Hubs;
using Blog.Core.Log;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Profiling;

namespace Blog.Core.Filter
{
    public class GlobalExceptionsFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;
        private readonly ILoggerHelper _loggerHelper;
        private readonly IHubContext<ChatHub> _hubContext;
        public GlobalExceptionsFilter(IHostingEnvironment env, ILoggerHelper loggerHelper,IHubContext<ChatHub> hubContext)
        {
            _env = env;
            _loggerHelper = loggerHelper;
            _hubContext = hubContext;
        }
        public void OnException(ExceptionContext context)
        {
            var json = new JsonErrorResponse();
            json.Message = context.Exception.Message;//错误信息
            string url = context.ActionDescriptor.AttributeRouteInfo.Template;//api地址
            //自定义接受到的字符串
            string strJson = string.Empty;
            //strJson += "Controller:" + context.RouteData.Values["controller"] + "\r\n";
            //strJson += "Action:"+ context.RouteData.Values["action"]+"\r\n";
            //context.RouteData.Values.Remove("action");
            //context.RouteData.Values.Remove("controller");
            //var result =   JsonConvert.SerializeObject(context.RouteData.Values);
            //strJson += result;
            //using (var mem = new MemoryStream())
            //using (var reader = new StreamReader(context.HttpContext.Request))
            //{
            //    context.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            //    context.HttpContext.Request.Body.CopyTo(mem);
            //    mem.Seek(0,SeekOrigin.Begin);
            //    var body = reader.ReadToEnd();
            //} 
            //Stream stream = context.HttpContext.Request.Body;
            //byte[] buffer = new byte[context.HttpContext.Request.ContentLength.Value];
            //stream.Read(buffer, 0, buffer.Length);
            //string content = Encoding.UTF8.GetString(buffer);
            //返回的JSON参数
            json.ResultJson = strJson;
            if (_env.IsDevelopment())
            {
                json.DevelopmentMessage = context.Exception.StackTrace;//堆栈信息
            }
            context.Result = new InternalServerErrorObjectResult(json);
            MiniProfiler.Current.CustomTiming("Errors：", json.Message);
            //采用log4net 进行错误日志记录
            _loggerHelper.Error(json.Message, WriteLog(json.Message, strJson, context.Exception));

            _hubContext.Clients.All.SendAsync("ReceiveUpdate", LogLock.GetLogData()).Wait();
        }
        /// <summary>
        /// 自定义返回格式
        /// </summary>
        /// <param name="throwMsg"></param>
        /// <param name="resultJson"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public string WriteLog(string throwMsg, string resultJson,Exception ex)
        {
            return string.Format("【自定义错误】：{0} \r\n【异常类型】：{1} \r\n【异常信息】：{2} \r\n【堆栈调用】：{3}\r\n【传入参数Json：】{4} \r\n ", new object[] { throwMsg,
                ex.GetType().Name, ex.Message, ex.StackTrace,resultJson });
        }
    }
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
    //返回错误信息
    public class JsonErrorResponse
    {
        /// <summary>
        /// 生产环境的消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 开发环境的消息
        /// </summary>
        public string DevelopmentMessage { get; set; }
        /// <summary>
        /// 传入Json参数
        /// </summary>
        public string ResultJson { get; set; }
    }
}
