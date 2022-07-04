using AutoMapper;
using OmegaSoftware.TestProject.BL.Domain.Models.DTOs;
using OmegaSoftware.TestProject.Web.Models.DTOs;

namespace OmegaSoftware.TestProject.Web.Mapping
{
    public class MappingPLConfiguration : Profile
    {
        public MappingPLConfiguration()
        {
            CreateMap<WheatherSubscriptionBLModel, WheatherSubscriptionPLModel>().ReverseMap();
            CreateMap<GoogleTranslateSubscriptionBLModel, GoogleTranslateSubscriptionPLModel>().ReverseMap();
            CreateMap<FootballSubscriptionBLModel, FootballSubscriptionPLModel>().ReverseMap();
        }
    }
}

