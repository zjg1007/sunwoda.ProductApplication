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

namespace Blog.Core.Services.PDASeriveces
{
    public class PdaService : BaseServices<AgitationModel>, IPdaService
    {
        IPdaRepository _dal;
        PMESWeb.MESInterfaceSoapClient pmes = new PMESWeb.MESInterfaceSoapClient(PMESWeb.MESInterfaceSoapClient.EndpointConfiguration.MESInterfaceSoap);
        IMapper _mapper;
        public PdaService(IMapper mapper, IPdaRepository dal)
        {
            this._mapper = mapper;
            _dal = dal;
            this.BaseDal = dal;
        }
        /// <summary>
        /// PDA版本信息
        /// </summary>
        /// <returns></returns>
        public async Task<MessageModel<List<APP_VersionModel>>> VersionInfo ()
        {
            MessageModel<List<APP_VersionModel>> message = new MessageModel<List<APP_VersionModel>>();
           message.response = await _dal.VersionInfo();
            message.success = true;
            message.msg = "获取信息成功";
            return message;
        }
    }
}
