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
    public class DesabilitarProdutoCommand : IRequest<ValidationResult>
    {
        // Log do evento
        public string OrigemRequisicao { get; set; }
        public string Uri { get; set; }

        // Produto
        public Guid Id { get; set; }
    }

    public class DesabilitarProdutoCommandHandler : IRequestHandler<DesabilitarProdutoCommand, ValidationResult>
    {
        public DesabilitarProdutoCommandHandler(IProdutoRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
            _validacoes = new DesabilitarProdutoCommandValidation();
        }

        private readonly IProdutoRepository _repository;
        private readonly IMediator _mediator;
        private readonly DesabilitarProdutoCommandValidation _validacoes;

        public async Task<ValidationResult> Handle(DesabilitarProdutoCommand request, CancellationToken cancellationToken)
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

    public class DesabilitarProdutoCommandValidation : AbstractValidator<DesabilitarProdutoCommand>
    {
        public DesabilitarProdutoCommandValidation()
        {
            RuleFor(_ => _.Id).NotEqual(Guid.Empty).WithMessage("Identificador inválido!");
        }
    }
}
