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
    public class AtualizarProdutoCommand : IRequest<ValidationResult>
    {
        public Guid Id { get; set; }
        public string Marca { get; set; }
        public string Nome { get; set; }
        public string Lote { get; set; }
        public DateTime Fabricacao { get; set; }
        public DateTime Vencimento { get; set; }
        public string Observacao { get; set; }
        public long Quantidade { get; set; }
    }

    public class AtualizarProdutoCommandHandler : IRequestHandler<AtualizarProdutoCommand, ValidationResult>
    {
        public AtualizarProdutoCommandHandler(IProdutoRepository repository, IMediator mediator)
        {
            _repository = repository;
            _validacoes = new AtualizarProdutoCommandValidation();
            _mediator = mediator;

            #region AutoMapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AtualizarProdutoCommand, Produto>();
            });

            _mapper = configuration.CreateMapper();
            #endregion
        }

        private readonly IProdutoRepository _repository;
        private readonly AtualizarProdutoCommandValidation _validacoes;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public async Task<ValidationResult> Handle(AtualizarProdutoCommand request, CancellationToken cancellationToken)
        {
            var valido = _validacoes.Validate(request);

            if (valido.IsValid)
            {
                var produto = _mapper.Map<Produto>(request);

                await _repository.Atualizar(produto);
                var sucesso = await _repository.UnitOfWork.Commit();

                await _mediator.Publish(new ProdutoCommitNotification(sucesso));
            }

            return await Task.FromResult(valido);
        }
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
