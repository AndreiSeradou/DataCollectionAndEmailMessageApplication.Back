using AutoMapper;
using OmegaSoftware.TestProject.BL.App.DTOs.Request;
using OmegaSoftware.TestProject.BL.App.DTOs.Responce;
using OmegaSoftware.TestProject.DAL.Models;

namespace OmegaSoftware.TestProject.BL.App.Mapping
{
    public class MappingBLConfiguration : Profile
    {
        public MappingBLConfiguration()
        {
            CreateMap<WheatherSubscriptionRequest, WheatherSubscriptionResponce>().ReverseMap();
            CreateMap<FootballSubscriptionRequest, FootballSubscriptionResponce>().ReverseMap();
            CreateMap<GoogleTranslateSubscriptionRequest, GoogleTranslateSubscriptionResponce>().ReverseMap();
            CreateMap<UserRequest, UserResponce>().ReverseMap();
            CreateMap<WheatherSubscriptionResponce, WheatherSubscription>().ReverseMap();
            CreateMap<FootballSubscriptionResponce, FootballSubscription>().ReverseMap();
            CreateMap<GoogleTranslateSubscriptionResponce, GoogleTranslateSubscription>().ReverseMap();
            CreateMap<UserResponce, User>().ReverseMap();
        }
    }
}
