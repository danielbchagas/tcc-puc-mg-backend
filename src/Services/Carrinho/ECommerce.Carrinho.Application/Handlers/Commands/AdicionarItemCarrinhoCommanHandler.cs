using ECommerce.Carrinho.Application.Commands;
using ECommerce.Carrinho.Application.Notifications;
using ECommerce.Carrinho.Application.Queries;
using ECommerce.Carrinho.Domain.Interfaces.Repositories;
using ECommerce.Carrinho.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Carrinho.Application.Handlers.Commands
{
    public class AdicionarItemCarrinhoCommanHandler : IRequestHandler<AdicionarItemCarrinhoCommand, ValidationResult>
    {
        private readonly ICarrinhoRepository _carrinhoRepository;
        private readonly IItemCarrinhoRepository _itemCarrinhoRepository;
        private readonly IMediator _mediator;

        public AdicionarItemCarrinhoCommanHandler(ICarrinhoRepository carrinhoRepository, IItemCarrinhoRepository itemCarrinhoRepository, IMediator mediator)
        {
            _carrinhoRepository = carrinhoRepository;
            _itemCarrinhoRepository = itemCarrinhoRepository;
            _mediator = mediator;
        }

        public async Task<ValidationResult> Handle(AdicionarItemCarrinhoCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResult();
            var success = false;
            
            var carrinho = await _mediator.Send(new BuscarCarrinhoQuery(request.CarrinhoId));
            var item = new ItemCarrinho(request.Nome, request.Quantidade, request.Valor, request.Imagem, request.ProdutoId, carrinho.Id);

            #region Adiciona o novo item ao carrinho
            validationResult = item.Validar();

            if (!validationResult.IsValid)
                return await Task.FromResult(validationResult);

            await _itemCarrinhoRepository.Adicionar(item);
            success = await _itemCarrinhoRepository.UnitOfWork.Commit();
            #endregion

            #region Atualiza o carrinho
            carrinho.Itens.Add(item);

            validationResult = carrinho.Validar();

            if (!validationResult.IsValid)
                return await Task.FromResult(validationResult);

            if (success)
            {
                await _carrinhoRepository.Atualizar(carrinho);
                success = await _carrinhoRepository.UnitOfWork.Commit();

                if (success)
                    await _mediator.Publish(new ItemCarrinhoCommitNotification(itemCarrinhoId: item.Id, clienteId: request.ClienteId));
            }
            #endregion

            return await Task.FromResult(validationResult);
        }
    }
}
