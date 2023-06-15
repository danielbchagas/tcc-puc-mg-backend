using ECommerce.Customers.Application.Commands.Customer;
using ECommerce.Customers.Domain.Interfaces.Data;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customers.Application.Handlers.Commands.Customer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, (ValidationResult, Domain.Models.Customer)>
    {
        public UpdateCustomerCommandHandler(ICustomerRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        private readonly ICustomerRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public async Task<(ValidationResult, Domain.Models.Customer)> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.Get(request.Id);
            UpdateCustomer(customer, request);
            UpdateDocument(customer, request.Document);
            UpdatePhone(customer, request.Phone);
            UpdateAddress(customer, request.Address);

            var validation = customer.Validate();

            if (validation.IsValid)
                await _unitOfWork.Commit();

            return await Task.FromResult((validation, customer));
        }

        private void UpdateCustomer(Domain.Models.Customer customer, UpdateCustomerCommand request) =>
            customer.UpdateCustomer(request.FirstName, request.LastName);

        private void UpdateDocument(Domain.Models.Customer customer, UpdateDocumentCommand request) =>
            customer.UpdateDocument(request.Number);

        private void UpdatePhone(Domain.Models.Customer customer, UpdatePhoneCommand request) =>
            customer.UpdatePhone(request.Number);

        private void UpdateAddress(Domain.Models.Customer customer, UpdateAddressCommand request)
        {
            if (request != null)
                customer.UpdateAddress(request.FirstLine, request.SecondLine, request.City, request.ZipCode, request.State);
        }
    }
}
