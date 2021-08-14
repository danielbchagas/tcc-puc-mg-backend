using ECommerce.Cliente.Domain.Enums;
using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Cliente.Domain.Application.Commands
{
    public class AdicionarEnderecoCommand : IRequest<ValidationResult>
    {
        public AdicionarEnderecoCommand(string logradouro, string bairro, string cidade, string cep, Estados estado, Guid clienteId)
        {
            Logradouro = logradouro;
            Bairro = bairro;
            Cidade = cidade;
            Cep = cep;
            Estado = estado;
            ClienteId = clienteId;
        }

        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public Estados Estado { get; set; }

        public Guid ClienteId { get; set; }
    }
}
