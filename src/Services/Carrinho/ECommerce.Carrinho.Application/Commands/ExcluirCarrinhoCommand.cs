using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Carrinho.Application.Commands
{
    public class ExcluirCarrinhoCommand : IRequest<ValidationResult>
    {
        public ExcluirCarrinhoCommand(Guid id, Guid clienteId)
        {
            Id = id;

            ClienteId = clienteId;
        }

        public Guid Id { get; set; }

        public Guid ClienteId { get; set; }
    }
}
