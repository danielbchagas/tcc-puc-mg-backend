using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Application.Queries;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using ECommerce.Core.Models.Basket;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Basket.Application.Handlers.Commands
{
    public class CreateBasketItemCommandHandler : IRequestHandler<CreateBasketItemCommand, ValidationResult>
    {
        private readonly ICustomerBasketRepository _basketRepository;
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly IMediator _mediator;

        public CreateBasketItemCommandHandler(ICustomerBasketRepository basketRepository, IBasketItemRepository basketItemRepository, IMediator mediator)
        {
            _basketRepository = basketRepository;
            _basketItemRepository = basketItemRepository;
            _mediator = mediator;
        }

        public async Task<ValidationResult> Handle(CreateBasketItemCommand request, CancellationToken cancellationToken)
        {
            var basket = await _mediator.Send(new GetCustomerBasketQuery(request.CustomerBasketId));
            var item = new BasketItem(request.Name, request.Quantity, request.Value, request.Image, request.ProductId, basket.Id);

            #region Adds new item to basket
            var validation = item.Validate();

            if (!validation.IsValid)
                return await Task.FromResult(validation);

            await _basketItemRepository.Create(item);
            var success = await _basketItemRepository.UnitOfWork.Commit();
            #endregion

            #region Updates basket
            basket.UpdatesItems(item);

            validation = basket.Validate();

            if (!validation.IsValid)
                return await Task.FromResult(validation);

            if (success)
            {
                await _basketRepository.Update(basket);
                await _basketRepository.UnitOfWork.Commit();
            }
            #endregion

            return await Task.FromResult(validation);
        }
    }
}
