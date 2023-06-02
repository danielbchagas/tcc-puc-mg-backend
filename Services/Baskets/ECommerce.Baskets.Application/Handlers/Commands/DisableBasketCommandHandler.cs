using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Basket.Application.Handlers.Commands
{
    public class DisableBasketCommandHandler : IRequestHandler<DisableBasketCommand, ValidationResult>
    {
        private readonly IBasketRepository _repository;
        
        public DisableBasketCommandHandler(IBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<ValidationResult> Handle(DisableBasketCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResult();

            var basket = await _repository.Get(request.Id);

            if(basket == null)
            {
                var errors = new List<ValidationFailure>();
                errors.Add(new ValidationFailure("", "Carrinho não encontrado."));
                return new ValidationResult(errors);
            }

            basket.DeletedAt = DateTime.Now;
            basket.IsEnded = false;

            await _repository.Update(basket);
            await _repository.UnitOfWork.Commit();

            return await Task.FromResult(validationResult);
        }
    }
}
