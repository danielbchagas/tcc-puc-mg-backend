using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Cliente.Domain.Application.Commands
{
    public class AtualizarClienteCommand : IRequest<ValidationResult>
    {
        public AtualizarClienteCommand(Guid id, string nome, string sobrenome, bool ativo)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Ativo = ativo;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public bool Ativo { get; set; }
    }
}
