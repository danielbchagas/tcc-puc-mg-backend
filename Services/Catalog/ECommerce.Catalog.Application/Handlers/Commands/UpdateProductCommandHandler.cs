using ECommerce.Catalog.Application.Commands;
using ECommerce.Catalog.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Application.Handlers.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ValidationResult>
    {
        private readonly IProductRepository _repository;

        public UpdateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ValidationResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var product = await _repository.Get(request.Id);

            product.Id = request.Id;
            product.Name = request.Name;
            product.Description = request.Description;
            product.Enabled = request.Enabled;
            product.Image = request.Image;
            product.Value = request.Value;
            product.Quantity = request.Quantity;

            validation = product.Validate();

            if (validation.IsValid)
            {
                await _repository.Update(product);
                await _repository.UnitOfWork.Commit();
            }

            return await Task.FromResult(validation);
        }
    }
}
