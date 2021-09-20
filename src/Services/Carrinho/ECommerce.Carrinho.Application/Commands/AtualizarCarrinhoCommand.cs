using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Carrinho.Application.Commands
{
    public class AtualizarCarrinhoCommand : IRequest<ValidationResult>
    {
        public AtualizarCarrinhoCommand(Guid id, Guid clienteId)
        {
            Id = id;
            ClienteId = clienteId;
        }

        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public Guid ClienteId { get; set; }
    }
}
