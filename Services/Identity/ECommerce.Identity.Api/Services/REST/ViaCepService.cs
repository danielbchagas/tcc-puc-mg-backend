using AutoMapper;
using ECommerce.Identity.Api.Constants;
using ECommerce.Identity.Api.Interfaces;
using ECommerce.Identity.Api.Models.Request;
using System;
using System.Threading.Tasks;

namespace ECommerce.Identity.Api.Services.REST
{
    public class ViaCepService : IViaCepService
    {
        private readonly IViaCepRequest _viaCepRequest;
        private readonly IMapper _mapper;

        public ViaCepService(IViaCepRequest viaCepRequest, IMapper mapper)
        {
            _viaCepRequest = viaCepRequest;
            _mapper = mapper;
        }

        /// <summary>
        /// A API do ViaCEP permite o acesso a dados dos CEPs brasileiros de forma simples e gratuita.
        /// A API retorna 200 mesmo quando o CEP não é encontrado, por isso é necessário verificar se o UF está vazio.
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<AddressRequest> GetAddress(string zipCode)
        {
            var response = await _viaCepRequest.Get(zipCode);

            if(response.Uf == null)
                throw new Exception(ResponseMessages.ADDRESS_NOT_FOUND);

            return _mapper.Map<AddressRequest>(response);
        }
    }
}
