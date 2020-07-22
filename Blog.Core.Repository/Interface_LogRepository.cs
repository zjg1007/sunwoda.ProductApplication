using System;
using System.Collections.Generic;
using System.Text;
using Blog.Core.IRepository;
using Blog.Core.IRepository.UnitOfWork;
using Blog.Core.Model.Models;
using Blog.Core.Repository.Base;

namespace Blog.Core.Repository
{
    public class Interface_LogRepository : BaseRepository<Interface_Log>, IInterface_LogRepository
    {
        public Interface_LogRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
