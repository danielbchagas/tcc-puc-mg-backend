using ECommerce.Gateway.Api.Enums;
using System;

namespace ECommerce.Gateway.Api.Models.Cliente
{
    public class EnderecoDto
    {
        public Guid Id { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public Estados Estado { get; set; }

        public Guid ClienteId { get; set; }
    }
}