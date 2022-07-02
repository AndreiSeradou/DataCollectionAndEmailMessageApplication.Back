using AutoMapper;
using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
using DataCollectionAndEmailMessageApplication.Web.Models.DTOs;

namespace DataCollectionAndEmailMessageApplication.Web.Mapping
{
    public class MappingPLConfiguration : Profile
    {
        public MappingPLConfiguration()
        {
            CreateMap<WheatherSubscriptionBLModel, WheatherSubscriptionPLModel>().ReverseMap();
        }
    }
}

