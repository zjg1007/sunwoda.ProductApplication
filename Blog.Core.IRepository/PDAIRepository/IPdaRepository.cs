using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.IRepository.Base;
using Blog.Core.Model.PDAModel;

namespace Blog.Core.IRepository.PDAIRepository
{
   public interface IPdaRepository : IBaseRepository<AgitationModel>
    {
        Task<List<APP_VersionModel>> VersionInfo();
    }
}
