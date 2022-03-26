using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using ECommerce.Basket.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace ECommerce.Basket.Application.Handlers.Commands
{
    public class UpdateBasketItemCommandHandler : IRequestHandler<UpdateBasketItemCommand, ValidationResult>
    {
        private readonly IShoppingBasketRepository _basketRepository;
        private readonly IBasketItemRepository _basketItemRepository;

        public UpdateBasketItemCommandHandler(IShoppingBasketRepository basketRepository, IBasketItemRepository basketItemRepository)
        {
            _basketRepository = basketRepository;
            _basketItemRepository = basketItemRepository;
        }

        public async Task<ValidationResult> Handle(UpdateBasketItemCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var basket = await _basketRepository.Get(request.ShoppingBasketId);
            
            if(basket == null)
            {
                var errors = new List<ValidationFailure>();
                errors.Add(new ValidationFailure("", "Carrinho não encontrado."));
                return new ValidationResult(errors);
            }

            var item = basket.Items.FirstOrDefault(i => i.ProductId == request.ProductId);

            if (item is null)
            {
                item = new BasketItem(id: request.Id, name: request.Name, quantity: request.Quantity, value: request.Value, image: request.Image, productId: request.ProductId, request.ShoppingBasketId);

                validation = item.Validate();

                if (!validation.IsValid)
                    return await Task.FromResult(validation);

                await _basketItemRepository.Create(item);
            }
            else
            {
                item.Quantity = request.Quantity;
            }

            #region Updates basket
            validation = basket.UpdatesItems(item);

            if (!validation.IsValid)
                return await Task.FromResult(validation);

            validation = basket.Validate();

            if (!validation.IsValid)
                return await Task.FromResult(validation);

            await _basketRepository.Update(basket);
            await _basketRepository.UnitOfWork.Commit();
            #endregion

            return await Task.FromResult(validation);
        }
    }
}
