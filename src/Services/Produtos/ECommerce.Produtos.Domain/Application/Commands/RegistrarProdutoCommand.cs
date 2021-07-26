using AutoMapper;
using ECommerce.Produtos.Domain.Application.Notifications;
using ECommerce.Produtos.Domain.Interfaces.Repositories;
using ECommerce.Produtos.Domain.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Produtos.Domain.Application.Commands
{
    public class RegistrarProdutoCommand : IRequest<ValidationResult>
    {
        public RegistrarProdutoCommand()
        {
            Id = Guid.NewGuid();
        }

        // Log do evento
        public string OrigemRequisicao { get; set; }
        public string Uri { get; set; }

        // Produto
        public Guid Id { get; private set; }
        public string Marca { get; set; }
        public string Nome { get; set; }
        public string Lote { get; set; }
        public DateTime Fabricacao { get; set; }
        public DateTime Vencimento { get; set; }
        public string Observacao { get; set; }
        public long Quantidade { get; set; }
    }

    public class RegistrarProdutoCommandValidation : AbstractValidator<RegistrarProdutoCommand>
    {
        public RegistrarProdutoCommandValidation()
        {
            RuleFor(_ => _.Marca).NotNull().NotEmpty().WithMessage("Informe o marca do produto!");
            RuleFor(_ => _.Nome).NotNull().NotEmpty().WithMessage("Informe o nome do produto!");
            RuleFor(_ => _.Lote).NotNull().NotEmpty().WithMessage("Informe o lote do produto!");
            RuleFor(_ => _.Quantidade).GreaterThanOrEqualTo(0).WithMessage("Limite atingido!");
            RuleFor(_ => _.Fabricacao).LessThanOrEqualTo(DateTime.Now).WithMessage("Data inválida!");
            RuleFor(_ => _.Vencimento).GreaterThanOrEqualTo(DateTime.Now).WithMessage("Produto vencido!");
        }
    }
}
