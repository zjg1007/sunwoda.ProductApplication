using AutoMapper;
using Blog.Core.Model.Models;
using Blog.Core.Model.PDAModel;
using Blog.Core.Model.PDAViewModel;
using Blog.Core.Model.ViewModels;

namespace Blog.Core.AutoMapper
{
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {
            CreateMap<BlogArticle, BlogViewModels>();
            CreateMap<GlueModel, GetShipCardReturns>();
            CreateMap<GlueSubmitParam, GlueSubmitRaturns>();
            CreateMap<AgitationModel, DeviceSelectReturns>();
            CreateMap<AgitationModel, GetShipCardReturns>()
                 .ForMember(d => d.shipCard, o => o.MapFrom(s => s.shipment_no));
            CreateMap<AgitationDetailParam, AgitationDetailModel>();
            CreateMap<LoginParam, AgitationLoad>()
                .ForMember(d => d.device_sn, o => o.MapFrom(s => s.deviceNo));
            CreateMap<GlueModel, GlueSubmitParam>();
            CreateMap<CloseParam, AgitationModel>();
            CreateMap<CoatModel, DeviceChangedReturns>();
            CreateMap<OldShipCartChangedParam, CoatModel>();
            CreateMap<AddMaterialParam, CoatModel>();
            CreateMap<CoatCloseParam, CoatModel>();
            CreateMap<CoatUpdateParam, CoatModel>();
            CreateMap<CoatSubmitParam, CoatModel>();
            CreateMap<GetShipCartCoatParam, CoatModel>();
            CreateMap<CoatModel, GetShipCartCoatReturns>();
            CreateMap<CoatModel, GetShipCartCoatParam>();

        }
    }
}
