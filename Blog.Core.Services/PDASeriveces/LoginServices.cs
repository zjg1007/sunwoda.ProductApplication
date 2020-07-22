using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Core.IRepository.PDAIRepository;
using Blog.Core.IServices.PDAIServices;
using Blog.Core.Model;
using Blog.Core.Model.PDAModel;
using Blog.Core.Model.PDAViewModel;
using Blog.Core.Services.BASE;
using PMESWeb;

namespace Blog.Core.Services.PDASeriveces
{
  public   class LoginServices : BaseServices<CheckNo_Login>, ILoginServices
    {
        IAgitationRepository _dal;
        IMapper _mapper;
        PMESWeb.MESInterfaceSoapClient pmes = new PMESWeb.MESInterfaceSoapClient(PMESWeb.MESInterfaceSoapClient.EndpointConfiguration.MESInterfaceSoap);
        public LoginServices(IMapper mapper, IAgitationRepository dal, ILoginRepository  Ldal)
        {
            this._mapper = mapper;
            _dal = dal;
            //通过父类的构造函数注入,这里是父类
            this.BaseDal = Ldal;
        }
        /// <summary>
        /// PDA用户登录
        /// </summary>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        public async Task<MessageModel<string>> Login(LoginParam jsondata)
        {
            MessageModel<string> message  = new Model.MessageModel<string>();
            //检验设备和工序是否对应
            if (jsondata.deviceNo.Substring(0, 1) == "Z" && jsondata.groupName.Substring(0, 2) != "正极")
            {
                message.success = false;
                message.msg = "正极设备号不能登陆负极项目";
                return message;
            }
            if (jsondata.deviceNo.Substring(0, 1) == "F" && jsondata.groupName.Substring(0, 2) != "负极")
            {
                message.success = false;
                message.msg = "负极设备号不能登陆正极项目";
                return message;
            }
            if (jsondata.deviceNo.Substring(2,1) == "C" && jsondata.groupName.Substring(0, 2) != "正极")
            {
                message.success = false;
                message.msg = "正极设备号不能登陆负极项目";
                return message;
            }
            if (jsondata.deviceNo.Substring(2, 1) == "A" && jsondata.groupName.Substring(0, 2) != "负极")
            {
                message.success = false;
                message.msg = "负极设备号不能登陆正极项目";
                return message;
            }
            AgitationLoad alModel = new AgitationLoad();
            alModel = _mapper.Map<AgitationLoad>(jsondata);
            var result1 = await _dal.GlueLoad(alModel);
            if (result1.Count <= 0) return message;
            
            if (result1[0].work_stationname.IndexOf(jsondata.groupName.Substring(jsondata.groupName.Length - 2, 2))==-1)
            {
                message.success = false;
                message.msg = $"该设备号不是【{jsondata.groupName}】设备号,不允许操作";
                return message;
            }


            //检查制令单是否有效
            var result= await base.SqlQuery<CheckNo_Login>(@" select s.project_id,
        (select distinct p.cell_model
         from t_co_package p
         where p.packagenumber = @packagenumber) cell_model
 from t_pm_project_base s, t_pm_mo_base n
 where  n.project_id = s.project_base_id
 and    n.close_flag in (2, 4)
 and    n.mo_number = @packagenumber", new { packagenumber = jsondata.packageNo });
            if (result.Count <=0)
            {
                message.success = false;
                message.msg = "制令单号没有上线或者不存在！";
                return message;
            }
            string res =   await pmes.CheckUserDoAsync(jsondata.username,jsondata.password,jsondata.deviceNo);
            if (res.ToUpper() != "TRUE")
            {
                message.success = false;
                message.msg = res;
                return message;
            }
            else
            {
                message.success = true;
                message.msg = "登录成功";
            }
            //message.response = jsondata;
            return message;
        }
    }
}
