using AutoMapper;
using OmegaSoftware.TestProject.BL.Domain.Models.DTOs;
using OmegaSoftware.TestProject.DAL.Models.DTOs;

namespace OmegaSoftware.TestProject.BL.Domain.Mapping
{
    public class MappingBLConfiguration : Profile
    {
        public MappingBLConfiguration()
        {
            CreateMap<WheatherSubscriptionBLModel, WheatherSubscription>().ReverseMap();
            CreateMap<FootballSubscriptionBLModel, FootballSubscription>().ReverseMap();
            CreateMap<GoogleTranslateSubscriptionBLModel, GoogleTranslateSubscription>().ReverseMap();
        }
    }
}
