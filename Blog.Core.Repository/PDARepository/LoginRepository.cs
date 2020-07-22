using System;
using System.Collections.Generic;
using System.Text;
using Blog.Core.IRepository.PDAIRepository;
using Blog.Core.IRepository.UnitOfWork;
using Blog.Core.Model.PDAModel;
using Blog.Core.Repository.Base;

namespace Blog.Core.Repository.PDARepository
{
    public  class LoginRepository : BaseRepository<CheckNo_Login>, ILoginRepository
    {
        public LoginRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
