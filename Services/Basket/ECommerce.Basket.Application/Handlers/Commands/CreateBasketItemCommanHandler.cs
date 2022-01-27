using System.Threading;
using System.Threading.Tasks;
using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Application.Queries;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using ECommerce.Basket.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Basket.Application.Handlers.Commands
{
    public class CreateBasketItemCommanHandler : IRequestHandler<CreateBasketItemCommand, ValidationResult>
    {
        private readonly ICustomerBasketRepository _cartRepository;
        private readonly IBasketItemRepository _itemRepository;
        private readonly IMediator _mediator;

        public CreateBasketItemCommanHandler(ICustomerBasketRepository cartRepository, IBasketItemRepository itemRepository, IMediator mediator)
        {
            _cartRepository = cartRepository;
            _itemRepository = itemRepository;
            _mediator = mediator;
        }

        public async Task<ValidationResult> Handle(CreateBasketItemCommand request, CancellationToken cancellationToken)
        {
            
            var cart = await _mediator.Send(new GetCustomerBasketQuery(request.CustomerBasketId));
            var item = new BasketItem(request.Name, request.Quantity, request.Value, request.Image, request.ProductId, cart.Id);

            #region Adds new item to basket
            var validation = item.Validate();

            if (!validation.IsValid)
                return await Task.FromResult(validation);

            await _itemRepository.Create(item);
            var success = await _itemRepository.UnitOfWork.Commit();
            #endregion

            #region Updates basket
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
