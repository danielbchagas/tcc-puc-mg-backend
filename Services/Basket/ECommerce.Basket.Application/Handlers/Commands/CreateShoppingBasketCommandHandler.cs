using System;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using ECommerce.Basket.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Basket.Application.Handlers.Commands
{
    public class CreateShoppingBasketCommandHandler : IRequestHandler<CreateShoppingBasketCommand, ValidationResult>
    {
        private readonly IShoppingBasketRepository _shoppingBasketRepository;
        
        public CreateShoppingBasketCommandHandler(IShoppingBasketRepository shoppingBasketRepository)
        {
            _shoppingBasketRepository = shoppingBasketRepository;
        }

        public async Task<ValidationResult> Handle(CreateShoppingBasketCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var basket = new ShoppingBasket(request.Id, request.CustomerId);

            validation = basket.Validate();

            if (!validation.IsValid)
                return await Task.FromResult(validation);

            await _shoppingBasketRepository.Create(basket);
            await _shoppingBasketRepository.UnitOfWork.Commit();

            return await Task.FromResult(validation);
        }
    }
}
