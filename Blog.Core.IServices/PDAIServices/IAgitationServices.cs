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
    public interface IAgitationServices : IBaseServices<t_gule_step_info>
    {
        #region 搅拌首页
        Task<MessageModel<List<AgitationModel>>> Load(AgitationLoad model);
        Task<DeviceSelectReturns> DeviceSelect(DeviceSelectParam model);
        Task<MessageModel<List<GroupKeyPressReturns>>> GroupKeyPress(GroupKeyPressParam model);
        Task<MessageModel<string>> PackageSelect(AgitationModel model);
        Task<MessageModel<GetShipCardReturns>> GetShipCarts(GetShipCardParam model);
        Task<MessageModel<AgitationSubmitParam>> Submit(AgitationSubmitParam model);
        Task<MessageModel<string>> Close(CloseParam model);
        Task<MessageModel<AgitationUpdateShipCartParam>> UpdateShipCart(AgitationUpdateShipCartParam model);

        #endregion
        #region 搅拌详细页
        /// <summary>
        ///搅拌当前步骤选择事件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<MessageModel<AgitationStrstepNameSelecteReturns>> StrstepNameSelecte(AgitationStrstepNameSelecteParam model);
        Task<MessageModel<AgitationItems>> GetItemInfo(AgitationGetItemInfoParam model);
        Task<MessageModel<AgitationDetailModel>> Submit(AgitationDetailParam model);
        #endregion

        Task<MessageModel<List<t_gule_step_infoVM>>> GetStepList(t_gule_step_infoVM model);
        Task<List<DeviceSelectReturns>> GetShipCartAll(DeviceSelectReturns model);

    }
}
