using ECommerce.Compras.Gateway.Enums;
using System;

namespace ECommerce.Compras.Gateway.Models.Cliente
{
    public class EnderecoDto
    {
        public Guid Id { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public Estados Estado { get; set; }
    }
}