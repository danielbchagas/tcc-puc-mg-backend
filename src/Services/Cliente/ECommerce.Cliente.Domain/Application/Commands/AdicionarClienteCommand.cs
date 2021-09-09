using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Cliente.Domain.Application.Commands
{
    public class AdicionarClienteCommand : IRequest<ValidationResult>
    {
        public AdicionarClienteCommand(Guid id, string nome, string sobrenome, bool ativo, string documento, string telefone, string email)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Ativo = ativo;
            Documento = documento;
            Telefone = telefone;
            Email = email;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public bool Ativo { get; set; }
        public string Documento { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}
