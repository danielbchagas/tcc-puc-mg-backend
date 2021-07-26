using ECommerce.Produtos.Domain.Application.Notifications;
using ECommerce.Produtos.Domain.Interfaces.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Produtos.Domain.Application.Commands
{
    public class ExcluirProdutoCommand : IRequest<ValidationResult>
    {
        // Log do evento
        public string OrigemRequisicao { get; set; }
        public string Uri { get; set; }

        // Produto
        public Guid Id { get; set; }
    }

    public class ExcluirProdutoCommandHandler : IRequestHandler<ExcluirProdutoCommand, ValidationResult>
    {
        public ExcluirProdutoCommandHandler(IProdutoRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
            _validacoes = new ExcluirProdutoCommandValidation();
        }

        private readonly IProdutoRepository _repository;
        private readonly IMediator _mediator;
        private readonly ExcluirProdutoCommandValidation _validacoes;

        public async Task<ValidationResult> Handle(ExcluirProdutoCommand request, CancellationToken cancellationToken)
        {
            var valido = _validacoes.Validate(request);

            if (valido.IsValid)
            {
                var produto = await _repository.Buscar(request.Id);

                produto.Desabilitar();

                var sucesso = await _repository.UnitOfWork.Commit();

                if (sucesso)
                    await _mediator.Publish(new ProdutoCommitNotification(request.OrigemRequisicao, request.Uri, request.Id));
            }

            return await Task.FromResult(valido);
        }
    }

    public class ExcluirProdutoCommandValidation : AbstractValidator<ExcluirProdutoCommand>
    {
        public ExcluirProdutoCommandValidation()
        {
            RuleFor(_ => _.Id).NotEqual(Guid.Empty).WithMessage("Identificador inválido!");
        }
    }
}
