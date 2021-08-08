using ECommerce.Catalogo.Domain.Application.Commands;
using ECommerce.Catalogo.Domain.Application.Notifications;
using ECommerce.Catalogo.Domain.Interfaces.Repositories;
using ECommerce.Catalogo.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Catalogo.Domain.Application.Handlers.Commands
{
    public class SubtrairProdutoCommandHandler : IRequestHandler<SubtrairProdutoCommand, ValidationResult>
    {
        public SubtrairProdutoCommandHandler(IProdutoRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
            _validacao = new ProdutoValidator();
        }

        private readonly IProdutoRepository _repository;
        private readonly IMediator _mediator;
        private readonly ProdutoValidator _validacao;

        public async Task<ValidationResult> Handle(SubtrairProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = await _repository.Buscar(request.Id);
            produto.Remover(request.Quantidade);

            var valido = _validacao.Validate(produto);

            if (valido.IsValid)
            {
                await _repository.Atualizar(produto);
                var sucesso = await _repository.UnitOfWork.Commit();

                if (sucesso)
                    await _mediator.Publish(new ProdutoCommitNotification(produtoId: request.Id, usuarioId: Guid.NewGuid())); // Trocar pelo ID do usuário da aplicação
            }

            return await Task.FromResult(valido);
        }
    }
}
