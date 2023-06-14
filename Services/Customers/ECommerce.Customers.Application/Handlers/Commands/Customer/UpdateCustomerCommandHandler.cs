using ECommerce.Customers.Application.Commands.Customer;
using ECommerce.Customers.Domain.Interfaces.Data;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customers.Application.Handlers.Commands.Customer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ValidationResult>
    {
        public UpdateCustomerCommandHandler(ICustomerRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        private readonly ICustomerRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public async Task<ValidationResult> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.Get(request.Id);
            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.UpdatedAt = DateTime.Now;

            var validation = customer.Validate();

            if (validation.IsValid)
            {
                await _repository.Update(customer);
                await _unitOfWork.Commit();
            }

            return await Task.FromResult(validation);
        }
    }
}
