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
    public interface ICoatServices : IBaseServices<CoatModel>
    {
        /// <summary>
        /// 设备基础信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<MessageModel<DeviceChangedReturns>> DeviceChanged(DeviceChangedParam model);
        Task<MessageModel<AgitationItems>> AddMaterial(AddMaterialParam model);
        Task<MessageModel<string>> Submit(CoatSubmitParam model);
        Task<MessageModel<string>> Close(CoatCloseParam model);
        Task<MessageModel<string>> UpdateShipCart(CoatUpdateParam model);
        Task<MessageModel<CheckUserRoleReturns>> CheckUserRole(CheckUserRoleparam model);
        Task<MessageModel<OldShipCartChangedReturns>> OldShipCartChanged(OldShipCartChangedParam model);
        Task<MessageModel<GetShipCartCoatReturns>> GetShipCartCoat(GetShipCartCoatParam model, int isum);

    }
}
