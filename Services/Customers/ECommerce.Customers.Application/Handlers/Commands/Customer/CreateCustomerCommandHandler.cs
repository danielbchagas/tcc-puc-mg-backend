using ECommerce.Customers.Application.Commands.Customer;
using ECommerce.Customers.Application.Services.REST;
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
        public CreateCustomerCommandHandler(ICustomerRepository repository, IUnitOfWork unitOfWork, IViaCepService viaCepService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _viaCepService = viaCepService;
        }

        private readonly ICustomerRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IViaCepService _viaCepService;

        public async Task<(ValidationResult, Models.Customer)> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await CreateCustomer(request);

            var validation = customer.Validate();

            if (validation.IsValid)
            {
                await _repository.Create(customer);
                await _unitOfWork.Commit();
            }

            return await Task.FromResult((validation, customer));
        }

        private async Task<Models.Customer> CreateCustomer(CreateCustomerCommand request)
        {
            var response = await _viaCepService.Get(request.ZipCode);
            
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
                address: new Models.Address
                {
                    FirstLine = response.logradouro,
                    SecondLine = response.complemento,
                    City = response.localidade,
                    State = response.uf,
                    ZipCode = request.ZipCode,
                    CustomerId = request.Id
                });
        }
    }
}
