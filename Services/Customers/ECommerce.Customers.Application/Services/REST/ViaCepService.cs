using AutoMapper;
using ECommerce.Customers.Domain.Models;
using Refit;
using System.Threading.Tasks;

namespace ECommerce.Customers.Application.Services.REST
{
    public class ViaCepService
    {
        private readonly IMapper _mapper;

        public ViaCepService()
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.CreateMap<ViaCepResponse, Address>()
                .ForMember(dest => dest.FirstLine, opt => opt.MapFrom(src => src.logradouro))
                .ForMember(dest => dest.SecondLine, opt => opt.MapFrom(src => src.complemento))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.localidade))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.uf))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.cep));
            });

            _mapper = configuration.CreateMapper();
        }

        public async Task<Address> GetAddress(string zipCode)
        {
            var viaCep = RestService.For<IViaCepService>("https://viacep.com.br/ws");
            var response = await viaCep.Get(zipCode);

            if (response != null)
                return _mapper.Map<Address>(response);

            return null;
        }
    }
}
