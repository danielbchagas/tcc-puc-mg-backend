using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Catalogo.Domain.Application.Commands
{
    public class SubtrairProdutoCommand : IRequest<ValidationResult>
    {
        public SubtrairProdutoCommand(Guid id, int quantidade)
        {
            Id = id;
            Quantidade = quantidade;
        }

        public Guid Id { get; set; }
        public int Quantidade { get; set; }
    }
}
