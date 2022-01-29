using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Basket.Application.Handlers.Commands
{
    public class UpdateBasketItemCommandHandler : IRequestHandler<UpdateBasketItemCommand, ValidationResult>
    {
        private readonly ICustomerBasketRepository _basketRepository;
        
        public UpdateBasketItemCommandHandler(ICustomerBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<ValidationResult> Handle(UpdateBasketItemCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var basket = await _basketRepository.Get(request.CustomerBasketId);
            var item = basket.Items.FirstOrDefault(i => i.Id == request.Id);

            if (item == null)
            {
                validation.Errors.Add(new ValidationFailure("", "Item não encontrado."));
                return validation;
            }

            item.Quantity = request.Quantity;
            basket.UpdatesItems(item);

            await _basketRepository.Update(basket);
            await _basketRepository.UnitOfWork.Commit();

            return await Task.FromResult(validation);
        }
    }
}
