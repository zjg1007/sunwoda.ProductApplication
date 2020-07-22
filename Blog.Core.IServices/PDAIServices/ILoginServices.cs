using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.IServices.BASE;
using Blog.Core.Model;
using Blog.Core.Model.PDAModel;
using Blog.Core.Model.PDAViewModel;

namespace Blog.Core.IServices.PDAIServices
{
   public interface ILoginServices : IBaseServices<CheckNo_Login>
    {
        Task<MessageModel<string>> Login(LoginParam jsondata);
    }
}
