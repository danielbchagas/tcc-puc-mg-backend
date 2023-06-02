using AutoMapper;
using ECommerce.Identity.Api.Models.Request;
using System;

namespace ECommerce.Identity.Api.Mappers
{
    public class ProtoProfile : Profile
    {
        public ProtoProfile()
        {
            CreateMap<SignUpUserRequest, Customers.Api.Protos.CreateUserRequest>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Document, opt => opt.MapFrom(src => new Customers.Api.Protos.Document
                {
                    Number = src.Document,
                    Userid = Convert.ToString(src.Id)
                }))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => new Customers.Api.Protos.Email
                {
                    Address = src.Email,
                    Userid = Convert.ToString(src.Id)
                }))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => new Customers.Api.Protos.Phone
                {
                    Number = src.Phone,
                    Userid = Convert.ToString(src.Id)
                }));
        }
    }
}
