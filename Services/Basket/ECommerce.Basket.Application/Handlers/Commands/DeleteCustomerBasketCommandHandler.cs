using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Basket.Application.Handlers.Commands
{
    public class DeleteCustomerBasketCommandHandler : IRequestHandler<DeleteCustomerBasketCommand, ValidationResult>
    {
        private readonly ICustomerBasketRepository _basketRepository;
        
        public DeleteCustomerBasketCommandHandler(ICustomerBasketRepository cartRepository)
        {
            _basketRepository = cartRepository;
        }

        public async Task<ValidationResult> Handle(DeleteCustomerBasketCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResult();

            var basket = await _basketRepository.Get(request.CustomerId);

            if(basket == null)
            {
                var errors = new List<ValidationFailure>();
                errors.Add(new ValidationFailure("", "Carrinho não encontrado."));
                return new ValidationResult(errors);
            }

            await _basketRepository.Delete(request.CustomerId);
            await _basketRepository.UnitOfWork.Commit();

            return await Task.FromResult(validationResult);
        }
    }
}
