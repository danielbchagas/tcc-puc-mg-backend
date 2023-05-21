using ECommerce.Customers.Application.Commands.Address;
using ECommerce.Customers.Application.Constants;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customers.Application.Handlers.Commands.Address
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

            var addresses = await _repository.Get(d => d.UserId == request.UserId);

            if (addresses.Any())
            {
                validation.Errors.Add(new ValidationFailure("", AddressErrorMessage.ADDRESS_EXISTS));

                return validation;
            }

            var address = new Domain.Models.Address(request.FirstLine, request.SecondLine, request.City, request.ZipCode, request.State, request.UserId);

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
