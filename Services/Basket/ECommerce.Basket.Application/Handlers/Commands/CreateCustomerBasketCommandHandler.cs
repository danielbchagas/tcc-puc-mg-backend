using System.Threading;
using System.Threading.Tasks;
using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using ECommerce.Basket.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Basket.Application.Handlers.Commands
{
    public class CreateCustomerBasketCommandHandler : IRequestHandler<CreateCustomerBasketCommand, ValidationResult>
    {
        private readonly ICustomerBasketRepository _customerBasketRepository;
        
        public CreateCustomerBasketCommandHandler(ICustomerBasketRepository customerBasketRepository)
        {
            _customerBasketRepository = customerBasketRepository;
        }

        public async Task<ValidationResult> Handle(CreateCustomerBasketCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var basket = new CustomerBasket(request.CustomerId);

            validation = basket.Validate();

            if (!validation.IsValid)
                return await Task.FromResult(validation);

            await _customerBasketRepository.Create(basket);
            await _customerBasketRepository.UnitOfWork.Commit();

            return await Task.FromResult(validation);
        }
    }
}
