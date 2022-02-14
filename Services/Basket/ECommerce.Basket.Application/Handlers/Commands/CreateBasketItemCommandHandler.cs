using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using ECommerce.Basket.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Basket.Application.Handlers.Commands
{
    public class CreateBasketItemCommandHandler : IRequestHandler<CreateBasketItemCommand, ValidationResult>
    {
        private readonly ICustomerBasketRepository _basketRepository;
        private readonly IBasketItemRepository _basketItemRepository;

        public CreateBasketItemCommandHandler(ICustomerBasketRepository basketRepository, IBasketItemRepository basketItemRepository)
        {
            _basketRepository = basketRepository;
            _basketItemRepository = basketItemRepository;
        }

        public async Task<ValidationResult> Handle(CreateBasketItemCommand request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.Get(request.CustomerBasketId);

            if(basket == null)
            {
                var errors = new List<ValidationFailure>();
                errors.Add(new ValidationFailure("", "Carrinho não encontrado."));
                return new ValidationResult(errors);
            }

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
