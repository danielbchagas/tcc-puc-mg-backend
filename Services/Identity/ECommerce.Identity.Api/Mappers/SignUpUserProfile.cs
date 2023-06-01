using AutoMapper;
using ECommerce.Identity.Api.Models.Request;

namespace ECommerce.Identity.Api.Mappers
{
    public class SignUpUserProfile : Profile
    {
        public SignUpUserProfile()
        {
            CreateMap<SignUpUserRequest, CustomerRequest>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForPath(dest => dest.Document, opt => opt.MapFrom(src => new DocumentRequest
                {
                    Number = src.Document,
                    UserId = src.Id
                }))
                .ForPath(dest => dest.Phone, opt => opt.MapFrom(src => new PhoneRequest
                {
                    Number = src.Phone,
                    UserId = src.Id
                }))
                .ForPath(dest => dest.Email, opt => opt.MapFrom(src => new EmailRequest 
                {
                    Address = src.Email,
                    UserId = src.Id
                }));
        }
    }
}
