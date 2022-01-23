using System;
using ECommerce.Cliente.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Cliente.Application.Commands
{
    public class CreateClienteCommand : IRequest<ValidationResult>
    {
        public CreateClienteCommand(Guid id, string nome, string sobrenome, bool ativo, Documento documento, Email email, Telefone telefone)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Ativo = ativo;

            Documento = documento;
            Email = email;
            Telefone = telefone;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public bool Ativo { get; set; }

        public Documento Documento { get; set; }
        public Email Email { get; set; }
        public Telefone Telefone { get; set; }
    }
}
