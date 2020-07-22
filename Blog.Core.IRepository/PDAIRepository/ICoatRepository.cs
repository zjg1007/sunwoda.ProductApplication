using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.IRepository.Base;
using Blog.Core.Model.PDAModel;
using Blog.Core.Model.PDAViewModel;

namespace Blog.Core.IRepository.PDAIRepository
{
    /// <summary>
    /// 涂布
    /// </summary>
   public  interface ICoatRepository : IBaseRepository<CoatModel>
    {
        Task<CoatModel> DeviceChanged(DeviceChangedParam model);
        Task<CoatModel> MeterialOverdue(CoatModel model);
        Task<CoatModel> ShipCartNumber(CoatModel model);
        Task<List<CoatModel>> ShipCartNoClose(CoatModel model);
        Task<CoatModel> GetShipCartNo(CoatModel model);
        Task<bool> UpdateShipCartNo(CoatModel model);
        Task<bool> AddShipCartNo(CoatModel model);
        Task<List<CoatModel>> MeterialByBom(CoatModel model);
        Task<bool> AddGroupName(CoatModel model);
        Task<bool> CoatUpdate(CoatModel model);
        Task<List<CoatModel>> GetShipcart(CoatModel model);
        Task<bool> AddShipcart(CoatModel model);
        Task<bool> DeteleShipcart(CoatModel model);
        Task<bool> UpShipcart(CoatModel model);
        Task<bool> AddMetrial(CoatModel model);
        Task<bool> Close(CoatModel model);
        Task<bool> CheckUserRole(CheckUserRoleparam model);
    }
}
