using AutoMapper;
using ECommerce.Identity.Api.Models.Request;

namespace ECommerce.Identity.Api.Mappers
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerRequest, Customers.Api.Protos.CreateUserRequest>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Document, opt => opt.MapFrom(src => src.Document))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone));
        }
    }
}
