using ECommerce.Customers.Application.Commands.User;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Models = ECommerce.Customers.Domain.Models;

namespace ECommerce.Customers.Application.Handlers.Commands.User
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ValidationResult>
    {
        public CreateCustomerCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        private readonly IUserRepository _repository;

        public async Task<ValidationResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Models.Customer(
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
                });

            var validation = customer.Validate();

            if (validation.IsValid)
            {
                await _repository.Create(customer);
                await _repository.UnitOfWork.Commit();
            }

            return await Task.FromResult(validation);
        }
    }
}
