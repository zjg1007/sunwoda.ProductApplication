using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.IRepository.PDAIRepository;
using Blog.Core.IRepository.UnitOfWork;
using Blog.Core.Model.PDAModel;
using Blog.Core.Repository.Base;

namespace Blog.Core.Repository.PDARepository
{
   public  class PdaRepository : BaseRepository<AgitationModel>, IPdaRepository
    {
        public PdaRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        /// <summary>
        /// 获取程序更新版本信息
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<APP_VersionModel>> VersionInfo()
        {
            string strAdd = string.Format(@"select * from T_MES_App_Update where app_name='PMES_PDA_ANDROID'");
            return await base.SqlQuery<APP_VersionModel>(strAdd, new { });
        }
    }
}
