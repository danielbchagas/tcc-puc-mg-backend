using ECommerce.Products.Application.Commands;
using ECommerce.Products.Domain.Interfaces.Data;
using ECommerce.Products.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Products.Application.Handlers.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ValidationResult>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ValidationResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var product = await _repository.Get(request.Id);

            product.Id = request.Id;
            product.Name = request.Name;
            product.Description = request.Description;
            product.Image = request.Image;
            product.Value = request.Value;
            product.Quantity = request.Quantity;
            product.UpdatedAt = DateTime.Now;

            validation = product.Validate();

            if (validation.IsValid)
            {
                await _repository.Update(product);
                await _unitOfWork.Commit();
            }

            return await Task.FromResult(validation);
        }
    }
}
