using AutoMapper;
using OmegaSoftware.TestProject.BL.Domain.Models.DTOs;
using OmegaSoftware.TestProject.DAL.Models;

namespace OmegaSoftware.TestProject.BL.Domain.Mapping
{
    public class MappingBLConfiguration : Profile
    {
        public MappingBLConfiguration()
        {
            CreateMap<WheatherSubscriptionDTOs, WheatherSubscription>().ReverseMap();
            CreateMap<FootballSubscriptionDTOs, FootballSubscription>().ReverseMap();
            CreateMap<GoogleTranslateSubscriptionDTOs, GoogleTranslateSubscription>().ReverseMap();
            CreateMap<UserDTOs, User>().ReverseMap();
        }
    }
}
