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
        private readonly ICustomerBasketRepository _cartRepository;
        
        public DeleteCustomerBasketCommandHandler(ICustomerBasketRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<ValidationResult> Handle(DeleteCustomerBasketCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResult();

            await _cartRepository.Delete(request.Id);
            await _cartRepository.UnitOfWork.Commit();

            return await Task.FromResult(validationResult);
        }
    }
}
