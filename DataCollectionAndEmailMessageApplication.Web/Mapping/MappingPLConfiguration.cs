using AutoMapper;
using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
using DataCollectionAndEmailMessageApplication.Web.Models.DTOs.Request;

namespace DataCollectionAndEmailMessageApplication.Web.Mapping
{
    public class MappingPLConfiguration : Profile
    {
        public MappingPLConfiguration()
        {
            CreateMap<WheatherSubscriptionBLModel, WheatherSubscribeRequest>().ReverseMap();
            CreateMap<WheatherSubscriptionBLModel, UpdateWheatherSubscriptionRequest>().ReverseMap();
        }
    }
}

