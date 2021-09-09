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
    public class AdicionarProdutoCommandHandler : IRequestHandler<AdicionarProdutoCommand, ValidationResult>
    {
        public AdicionarProdutoCommandHandler(IProdutoRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        private readonly IProdutoRepository _repository;
        private readonly IMediator _mediator;

        public async Task<ValidationResult> Handle(AdicionarProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = new Produto(request.Descricao, request.Nome, request.Imagem, request.QuantidadeEstoque, request.Preco, request.Ativo);

            var valido = produto.Validar();

            if (valido.IsValid)
            {
                await _repository.Adicionar(produto);
                var sucesso = await _repository.UnitOfWork.Commit();

                if (sucesso)
                    await _mediator.Publish(new ProdutoCommitNotification(produtoId: request.Id, usuarioId: Guid.NewGuid())); // Trocar pelo ID do usuário da aplicação
            }

            return await Task.FromResult(valido);
        }
    }
}
