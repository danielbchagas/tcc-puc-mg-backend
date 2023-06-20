using AutoMapper;
using ECommerce.Identity.Api.Models.Request;
using ECommerce.Identity.Api.Models.Response;

namespace ECommerce.Identity.Api.Mappers
{
    public class ViaCepResponseToAddressRequestProfile : Profile
    {
        public ViaCepResponseToAddressRequestProfile()
        {
            CreateMap<ViaCepResponse, AddressRequest>()
                .ForMember(dest => dest.FirstLine, opt => opt.MapFrom(src => src.Logradouro))
                .ForMember(dest => dest.SecondLine, opt => opt.MapFrom(src => src.Complemento))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Localidade))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Uf))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Cep));
        }
    }
}
