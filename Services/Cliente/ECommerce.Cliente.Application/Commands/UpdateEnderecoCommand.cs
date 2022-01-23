using System;
using ECommerce.Cliente.Domain.Enums;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Cliente.Application.Commands
{
    public class UpdateEnderecoCommand : IRequest<ValidationResult>
    {
        public UpdateEnderecoCommand(Guid id, string logradouro, string bairro, string cidade, string cep, Estados estado, Guid clienteId)
        {
            Id = id;
            Logradouro = logradouro;
            Bairro = bairro;
            Cidade = cidade;
            Cep = cep;
            Estado = estado;

            ClienteId = clienteId;
        }

        public Guid Id { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public Estados Estado { get; set; }

        public Guid ClienteId { get; set; }
    }
}
