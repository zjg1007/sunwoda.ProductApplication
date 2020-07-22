using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.IServices.BASE;
using Blog.Core.Model.Models;
using Blog.Core.Model.PDAModel;

namespace Blog.Core.IServices
{
    public interface IInterface_LogServices : IBaseServices<Interface_Log> 
    {
        Task<List<Interface_Log>> GetTestInterfaceLogData();
    }
}
