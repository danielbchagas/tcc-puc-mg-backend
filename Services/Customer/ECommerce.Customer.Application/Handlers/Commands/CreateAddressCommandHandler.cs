using ECommerce.Core.Models.Customer;
using ECommerce.Customer.Application.Commands;
using ECommerce.Customer.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customer.Application.Handlers.Commands
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, ValidationResult>
    {
        public CreateAddressCommandHandler(IAddressRepository repository)
        {
            _repository = repository;
        }

        private readonly IAddressRepository _repository;
        
        public async Task<ValidationResult> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var addresses = await _repository.Get(d => d.CustomerId == request.CustomerId);

            if (addresses.Any())
            {
                validation.Errors.Add(new ValidationFailure("", "O cliente já possui um endereço cadastrado."));

                return validation;
            }

            var address = new Address(request.FirstLine, request.SecondLine, request.City, request.ZipCode, request.State, request.CustomerId);

            validation = address.Validate();

            if (validation.IsValid)
            {
                await _repository.Create(address);
                await _repository.UnitOfWork.Commit();
            }

            return await Task.FromResult(validation);
        }
    }
}
