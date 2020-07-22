using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Core.IRepository;
using Blog.Core.IServices;
using Blog.Core.Model.Models;
using Blog.Core.Services.BASE;

namespace Blog.Core.Services
{
    public class Interface_LogSeriveces : BaseServices<Interface_Log>, IInterface_LogServices
    {
        IInterface_LogRepository _dal;
        IMapper _mapper;
        public Interface_LogSeriveces(IInterface_LogRepository dal, IMapper mapper)
        {
            this._dal = dal;
            base.BaseDal = dal;
            this._mapper = mapper;
        }
        public Interface_LogSeriveces()
        {

        }
        /// <summary>
        /// 获取日志列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Interface_Log>> GetTestInterfaceLogData()
        {
            var interfacelist = await base.Query(m=>int.Parse( m.LOGID)>0,0,10, "LOGID");
            return interfacelist;
        }
    }
}
