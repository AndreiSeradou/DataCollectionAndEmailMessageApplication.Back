using AutoMapper;

namespace DataCollectionAndEmailMessageApplication.Web.Mapping
{
    public class MappingPLConfiguration : Profile
    {
        public MappingPLConfiguration()
        {
            CreateMap<OrderBLModel, OrderResponse>().ReverseMap();
            CreateMap<BookBLModel, BookResponse>().ReverseMap();
            CreateMap<BookBLModel, BookRequest>().ReverseMap();
            CreateMap<Book, BookResponse>().ReverseMap();
            CreateMap<UserResponse, User>().ReverseMap();
        }
    }
}

