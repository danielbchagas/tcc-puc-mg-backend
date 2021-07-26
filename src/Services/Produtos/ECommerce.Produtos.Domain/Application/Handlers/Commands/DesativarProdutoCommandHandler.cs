using ECommerce.Produtos.Domain.Application.Commands;
using ECommerce.Produtos.Domain.Application.Notifications;
using ECommerce.Produtos.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Produtos.Domain.Application.Handlers.Commands
{
    public class DesativarProdutoCommandHandler : IRequestHandler<DesativarProdutoCommand, ValidationResult>
    {
        public DesativarProdutoCommandHandler(IProdutoRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
            _validacoes = new DesativarProdutoCommandValidation();
        }

        private readonly IProdutoRepository _repository;
        private readonly IMediator _mediator;
        private readonly DesativarProdutoCommandValidation _validacoes;

        public async Task<ValidationResult> Handle(DesativarProdutoCommand request, CancellationToken cancellationToken)
        {
            var valido = _validacoes.Validate(request);

            if (valido.IsValid)
            {
                var produto = await _repository.Buscar(request.Id);

                produto.Desativar();

                var sucesso = await _repository.UnitOfWork.Commit();

                if (sucesso)
                    await _mediator.Publish(new ProdutoCommitNotification(request.OrigemRequisicao, request.Uri, request.Id));
            }

            return await Task.FromResult(valido);
        }
    }
}
