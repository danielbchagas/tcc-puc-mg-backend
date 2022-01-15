using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Carrinho.Application.Commands
{
    public class AdicionarCarrinhoCommand : IRequest<ValidationResult>
    {
        public AdicionarCarrinhoCommand(Guid id, decimal valor, Guid clienteId)
        {
            Id = id;
            Valor = valor;

            ClienteId = clienteId;
        }

        public Guid Id { get; set; }
        public decimal Valor { get; set; }

        public Guid ClienteId { get; set; }
    }
}
