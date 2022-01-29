using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Basket.Application.Handlers.Commands
{
    public class DeleteBasketItemCommandHandler : IRequestHandler<DeleteBasketItemCommand, ValidationResult>
    {
        private readonly IBasketItemRepository _itemRepository;
        private readonly ICustomerBasketRepository _basketRepository;
        
        public DeleteBasketItemCommandHandler(IBasketItemRepository itemRepository, ICustomerBasketRepository basketRepository)
        {
            _itemRepository = itemRepository;
            _basketRepository = basketRepository;
        }

        public async Task<ValidationResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var basket = await _basketRepository.Get(request.CustomerBasketId);
            var item = basket.Items.FirstOrDefault(i => i.Id == request.Id);
            
            if(item == null)
                validation.Errors.Add(new ValidationFailure("", "Item não encontrado."));
            else
            {
                item.Quantity = request.Quantity;
                basket.UpdatesItems(item);

                await _itemRepository.Update(item);
                var success = await _itemRepository.UnitOfWork.Commit();

                if (success)
                {
                    await _basketRepository.Update(basket);
                    success = await _basketRepository.UnitOfWork.Commit();
                }
            }

            return await Task.FromResult(validation);
        }
    }
}
