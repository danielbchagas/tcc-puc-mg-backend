using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Basket.Application.Handlers.Commands
{
    public class CreateBasketCommandHandler : IRequestHandler<CreateBasketCommand, (ValidationResult, Domain.Models.Basket)>
    {
        private readonly IBasketRepository _repository;
        
        public CreateBasketCommandHandler(IBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<(ValidationResult, Domain.Models.Basket)> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var basket = new Domain.Models.Basket(request.Id, request.CustomerId);

            validation = basket.Validate();

            if (!validation.IsValid)
                return (await Task.FromResult(validation), null);

            await _repository.Create(basket);
            await _repository.UnitOfWork.Commit();

            return (await Task.FromResult(validation), basket);
        }
    }
}
