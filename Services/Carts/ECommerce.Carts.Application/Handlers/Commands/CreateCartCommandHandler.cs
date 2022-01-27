using System.Threading;
using System.Threading.Tasks;
using ECommerce.Carts.Application.Commands;
using ECommerce.Carts.Domain.Interfaces.Repositories;
using ECommerce.Carts.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Carts.Application.Handlers.Commands
{
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, ValidationResult>
    {
        private readonly ICartRepository _cartRepository;
        
        public CreateCartCommandHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<ValidationResult> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var cart = new Cart(request.CustomerId);

            validation = cart.Validate();

            if (!validation.IsValid)
                return await Task.FromResult(validation);

            await _cartRepository.Create(cart);
            await _cartRepository.UnitOfWork.Commit();

            return await Task.FromResult(validation);
        }
    }
}
