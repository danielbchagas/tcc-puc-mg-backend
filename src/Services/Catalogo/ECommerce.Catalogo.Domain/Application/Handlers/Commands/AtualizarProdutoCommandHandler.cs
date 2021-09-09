using ECommerce.Catalogo.Domain.Application.Commands;
using ECommerce.Catalogo.Domain.Application.Notifications;
using ECommerce.Catalogo.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Catalogo.Domain.Application.Handlers.Commands
{
    public class AtualizarProdutoCommandHandler : IRequestHandler<AtualizarProdutoCommand, ValidationResult>
    {
        public AtualizarProdutoCommandHandler(IProdutoRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        private readonly IProdutoRepository _repository;
        private readonly IMediator _mediator;

        public async Task<ValidationResult> Handle(AtualizarProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = await _repository.Buscar(request.Id);
            produto.Nome = request.Nome;
            produto.Ativo = request.Ativo;
            produto.Descricao = request.Descricao;
            produto.Imagem = request.Imagem;
            produto.Preco = request.Preco;
            produto.QuantidadeEstoque = request.QuantidadeEstoque;

            var valido = produto.Validar();

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
