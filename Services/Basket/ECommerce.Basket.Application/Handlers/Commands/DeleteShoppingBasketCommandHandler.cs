using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Basket.Application.Handlers.Commands
{
    public class DeleteShoppingBasketCommandHandler : IRequestHandler<DeleteShoppingBasketCommand, ValidationResult>
    {
        private readonly IShoppingBasketRepository _basketRepository;
        
        public DeleteShoppingBasketCommandHandler(IShoppingBasketRepository cartRepository)
        {
            _basketRepository = cartRepository;
        }

        public async Task<ValidationResult> Handle(DeleteShoppingBasketCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResult();

            var basket = await _basketRepository.Get(request.Id);

            if(basket == null)
            {
                var errors = new List<ValidationFailure>();
                errors.Add(new ValidationFailure("", "Carrinho não encontrado."));
                return new ValidationResult(errors);
            }

            await _basketRepository.Delete(request.Id);
            await _basketRepository.UnitOfWork.Commit();

            return await Task.FromResult(validationResult);
        }
    }
}
