using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Basket.Application.Handlers.Commands
{
    public class DeleteBasketItemCommandHandler : IRequestHandler<DeleteBasketItemCommand, ValidationResult>
    {
        private readonly ICustomerBasketRepository _basketRepository;
        private readonly IBasketItemRepository _basketItemRepository;

        public DeleteBasketItemCommandHandler(ICustomerBasketRepository basketRepository, IBasketItemRepository basketItemRepository)
        {
            _basketRepository = basketRepository;
            _basketItemRepository = basketItemRepository;
        }

        public async Task<ValidationResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var item = await _basketItemRepository.Get(request.Id);
            
            if (item == null)
            {
                validation.Errors.Add(new ValidationFailure("", "Item não encontrado."));
                return validation;
            }

            // Use item to get basket
            var basket = await _basketRepository.Get(item.CustomerBasketId);

            // If item found, remove from basket
            basket.RemoveItem(item);

            // Remove item and update the basket
            await _basketItemRepository.Delete(item.Id);
            await _basketRepository.Update(basket);
            
            await _basketRepository.UnitOfWork.Commit();

            return await Task.FromResult(validation);
        }
    }
}
