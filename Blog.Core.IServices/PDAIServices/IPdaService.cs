using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.IServices.BASE;
using Blog.Core.Model;
using Blog.Core.Model.PDAModel;

namespace Blog.Core.IServices.PDAIServices
{
    public interface IPdaService : IBaseServices<AgitationModel>
    {
        Task<MessageModel<List<APP_VersionModel>>> VersionInfo();
    }
}
