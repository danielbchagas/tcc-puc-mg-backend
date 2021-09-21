using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Carrinho.Application.Commands
{
    public class ExcluirItemCarrinhoCommand : IRequest<ValidationResult>
    {
        public ExcluirItemCarrinhoCommand(Guid id, Guid clienteId)
        {
            Id = id;

            ClienteId = clienteId;
        }

        public Guid Id { get; set; }

        public Guid ClienteId { get; set; }
    }
}
