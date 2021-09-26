using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Carrinho.Application.Commands
{
    public class ExcluirCarrinhoCommand : IRequest<ValidationResult>
    {
        public ExcluirCarrinhoCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
