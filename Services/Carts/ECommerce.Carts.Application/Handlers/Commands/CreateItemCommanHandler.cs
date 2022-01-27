using System.Threading;
using System.Threading.Tasks;
using ECommerce.Carts.Application.Commands;
using ECommerce.Carts.Application.Queries;
using ECommerce.Carts.Domain.Interfaces.Repositories;
using ECommerce.Carts.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Carts.Application.Handlers.Commands
{
    public class CreateItemCommanHandler : IRequestHandler<CreateItemCommand, ValidationResult>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMediator _mediator;

        public CreateItemCommanHandler(ICartRepository cartRepository, IItemRepository itemRepository, IMediator mediator)
        {
            _cartRepository = cartRepository;
            _itemRepository = itemRepository;
            _mediator = mediator;
        }

        public async Task<ValidationResult> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            
            var cart = await _mediator.Send(new GetCartQuery(request.CartId));
            var item = new Item(request.Name, request.Quantity, request.Value, request.Image, request.ProductId, cart.Id);

            #region Adds new item to cart
            var validation = item.Validate();

            if (!validation.IsValid)
                return await Task.FromResult(validation);

            await _itemRepository.Create(item);
            var success = await _itemRepository.UnitOfWork.Commit();
            #endregion

            #region Updates cart
            cart.Itens.Add(item);

            validation = cart.Validate();

            if (!validation.IsValid)
                return await Task.FromResult(validation);

            if (success)
            {
                await _cartRepository.Update(cart);
                await _cartRepository.UnitOfWork.Commit();
            }
            #endregion

            return await Task.FromResult(validation);
        }
    }
}
