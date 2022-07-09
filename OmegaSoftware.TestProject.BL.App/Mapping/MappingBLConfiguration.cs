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
            CreateMap<SubscriptionRequest, SubscriptionResponce>().ReverseMap();
            CreateMap<UserRequest, UserResponce>().ReverseMap();
            CreateMap<SubscriptionRequest, Subscription>().ReverseMap();
            CreateMap<SubscriptionResponce, Subscription>().ReverseMap();
            CreateMap<UserResponce, User>().ReverseMap();
            CreateMap<ApiRequest, ApiResponce>().ReverseMap();
            CreateMap<ApiRequest, Api>().ReverseMap();
        }
    }
}
