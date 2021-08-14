using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Cliente.Domain.Application.Commands
{
    public class AtualizarClienteCommand : IRequest<ValidationResult>
    {
        public AtualizarClienteCommand(Guid id, string nome, string sobrenome, string documento, string telefone)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Documento = documento;
            Telefone = telefone;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Documento { get; set; }
        public string Telefone { get; private set; }
    }
}
