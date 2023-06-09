using ECommerce.Baskets.Application.Commands.Basket;
using ECommerce.Baskets.Application.Constants;
using ECommerce.Baskets.Domain.Interfaces.Data;
using ECommerce.Baskets.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Baskets.Application.Handlers.Commands.Basket
{
    public class DisableBasketCommandHandler : IRequestHandler<DisableBasketCommand, ValidationResult>
    {
        private readonly IBasketRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DisableBasketCommandHandler(IBasketRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ValidationResult> Handle(DisableBasketCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResult();

            var basket = await _repository.Get(request.Id);

            if (basket == null)
            {
                var errors = new List<ValidationFailure>();
                errors.Add(new ValidationFailure("", BasketMessages.BasketNotFound));
                return new ValidationResult(errors);
            }

            basket.EndBasket();

            await _repository.Update(basket);
            await _unitOfWork.Commit();

            return await Task.FromResult(validationResult);
        }
    }
}
