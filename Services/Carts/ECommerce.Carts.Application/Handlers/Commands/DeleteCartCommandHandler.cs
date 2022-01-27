using System.Threading;
using System.Threading.Tasks;
using ECommerce.Carts.Application.Commands;
using ECommerce.Carts.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Carts.Application.Handlers.Commands
{
    public class DeleteCartCommandHandler : IRequestHandler<DeleteCartCommand, ValidationResult>
    {
        private readonly ICartRepository _cartRepository;
        
        public DeleteCartCommandHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<ValidationResult> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResult();

            await _cartRepository.Delete(request.Id);
            await _cartRepository.UnitOfWork.Commit();

            return await Task.FromResult(validationResult);
        }
    }
}
