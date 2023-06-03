using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Basket.Application.Handlers.Commands
{
    public class UpdateBasketCommandHandler : IRequestHandler<UpdateBasketCommand, (ValidationResult, Domain.Models.Basket)>
    {
        private readonly IBasketRepository _repository;

        public UpdateBasketCommandHandler(IBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<(ValidationResult, Domain.Models.Basket)> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var basket = await _repository.Get(request.Id);

            validation = basket.Validate();

            if (!validation.IsValid)
                return (await Task.FromResult(validation), basket);

            await _repository.Update(basket);
            await _repository.UnitOfWork.Commit();

            return (await Task.FromResult(validation), basket);
        }
    }
}
