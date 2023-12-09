using ECommerce.Customers.Application.Commands.Customer;
using ECommerce.Customers.Domain.Interfaces.Data;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Models = ECommerce.Customers.Domain.Models;

namespace ECommerce.Customers.Application.Handlers.Commands.Customer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, (ValidationResult, Models.Customer)>
    {
        public CreateCustomerCommandHandler(ICustomerRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        private readonly ICustomerRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public async Task<(ValidationResult, Models.Customer)> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            if(!request.Validate().IsValid)
                return await Task.FromResult((request.Validate(), default(Models.Customer)));

            var customer = ToCustomer(request);

            var validation = customer.Validate();

            if (validation.IsValid)
            {
                await _repository.Create(customer);
                await _unitOfWork.Commit();
            }

            return await Task.FromResult((validation, customer));
        }

        private static Models.Customer ToCustomer(CreateCustomerCommand request)
        {
            return new Models.Customer(
                id: request.Id,
                firstName: request.FirstName,
                lastName: request.LastName,
                createdAt: DateTime.Now,
                document: new Models.Document
                {
                    Number = request.Document.Number,
                    CustomerId = request.Document.CustomerId
                },
                email: new Models.Email
                {
                    Address = request.Email.Address,
                    CustomerId = request.Email.CustomerId
                },
                phone: new Models.Phone
                {
                    Number = request.Phone.Number,
                    CustomerId = request.Phone.CustomerId
                },
                address: request.Address != null ? new Models.Address
                {
                    FirstLine = request.Address.FirstLine,
                    SecondLine = request.Address.SecondLine,
                    City = request.Address.City,
                    State = request.Address.State,
                    ZipCode = request.Address.ZipCode,
                    CustomerId = request.Address.CustomerId
                } : null);
        }
    }
}
