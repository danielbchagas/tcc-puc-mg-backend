using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Basket.Application.Handlers.Commands
{
    public class UpdateShoppingBasketCommandHandler : IRequestHandler<UpdateShoppingBasketCommand, ValidationResult>
    {
        private readonly IShoppingBasketRepository _shoppingBasketRepository;

        public UpdateShoppingBasketCommandHandler(IShoppingBasketRepository shoppingBasketRepository)
        {
            _shoppingBasketRepository = shoppingBasketRepository;
        }

        public async Task<ValidationResult> Handle(UpdateShoppingBasketCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var basket = await _shoppingBasketRepository.Get(request.Id);

            validation = basket.Validate();

            // Finalize basket?
            if (request.IsEnded)
                validation = basket.Finalize();

            if (!validation.IsValid)
                return await Task.FromResult(validation);

            await _shoppingBasketRepository.Update(basket);
            await _shoppingBasketRepository.UnitOfWork.Commit();

            return await Task.FromResult(validation);
        }
    }
}
