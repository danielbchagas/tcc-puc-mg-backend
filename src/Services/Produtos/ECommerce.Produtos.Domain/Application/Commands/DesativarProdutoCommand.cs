using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Produtos.Domain.Application.Commands
{
    public class DesativarProdutoCommand : IRequest<ValidationResult>
    {
        // Log do evento
        public string OrigemRequisicao { get; set; }
        public string Uri { get; set; }

        // Produto
        public Guid Id { get; set; }
    }

    public class DesativarProdutoCommandValidation : AbstractValidator<DesativarProdutoCommand>
    {
        public DesativarProdutoCommandValidation()
        {
            RuleFor(_ => _.Id).NotEqual(Guid.Empty).WithMessage("Identificador inválido!");
        }
    }
}
