using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Produtos.Domain.Application.Commands
{
    public class AtualizarProdutoCommand : IRequest<ValidationResult>
    {
        // Log do evento
        public string OrigemRequisicao { get; set; }
        public string Uri { get; set; }

        // Produto
        public Guid Id { get; set; }
        public string Marca { get; set; }
        public string Nome { get; set; }
        public string Lote { get; set; }
        public DateTime Fabricacao { get; set; }
        public DateTime Vencimento { get; set; }
        public string Observacao { get; set; }
        public long Quantidade { get; set; }
    }

    public class AtualizarProdutoCommandValidation : AbstractValidator<AtualizarProdutoCommand>
    {
        public AtualizarProdutoCommandValidation()
        {
            RuleFor(_ => _.Id).NotEqual(Guid.Empty).WithMessage("Identificador inválido!");
            RuleFor(_ => _.Marca).NotEmpty().NotEmpty().WithMessage("Informe o marca do produto!");
            RuleFor(_ => _.Nome).NotEmpty().NotEmpty().WithMessage("Informe o nome do produto!");
            RuleFor(_ => _.Lote).NotEmpty().NotEmpty().WithMessage("Informe o lote do produto!");
            RuleFor(_ => _.Quantidade).GreaterThanOrEqualTo(0).WithMessage("Limite atingido!");
            RuleFor(_ => _.Fabricacao).LessThanOrEqualTo(DateTime.Now).WithMessage("Data inválida!");
            RuleFor(_ => _.Vencimento).GreaterThanOrEqualTo(DateTime.Now).WithMessage("Produto vencido!");
        }
    }
}
