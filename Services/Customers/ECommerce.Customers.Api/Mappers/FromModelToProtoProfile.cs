using AutoMapper;

namespace ECommerce.Customer.Api.Mappers
{
    public class FromModelToProtoProfile : Profile
    {
        public FromModelToProtoProfile()
        {
            CreateMap<Customers.Domain.Models.Customer, Customers.Api.Protos.Customer>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.LastName));

            CreateMap<Customers.Domain.Models.Document, Customers.Api.Protos.Document>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Customerid, opt => opt.MapFrom(src => src.CustomerId));

            CreateMap<Customers.Domain.Models.Email, Customers.Api.Protos.Email>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Customerid, opt => opt.MapFrom(src => src.CustomerId));

            CreateMap<Customers.Domain.Models.Phone, Customers.Api.Protos.Phone>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Customerid, opt => opt.MapFrom(src => src.CustomerId));
        }
    }
}
