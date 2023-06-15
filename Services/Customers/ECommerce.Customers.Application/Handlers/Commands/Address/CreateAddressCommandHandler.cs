using ECommerce.Customers.Application.Commands.Address;
using ECommerce.Customers.Application.Constants;
using ECommerce.Customers.Domain.Interfaces.Data;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customers.Application.Handlers.Commands.Address
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, (ValidationResult, Domain.Models.Address)>
    {
        public CreateAddressCommandHandler(IAddressRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        private readonly IAddressRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public async Task<(ValidationResult, Domain.Models.Address)> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var addresses = await _repository.Get(d => d.CustomerId == request.CustomerId);

            if (addresses.Any())
            {
                validation.Errors.Add(new ValidationFailure("", AddressErrorMessage.ADDRESS_EXISTS));

                return (validation, null);
            }

            var address = new Domain.Models.Address(request.FirstLine, request.SecondLine, request.City, request.ZipCode, request.State, request.CustomerId);

            validation = address.Validate();

            if (validation.IsValid)
            {
                await _repository.Create(address);
                await _unitOfWork.Commit();
            }

            return await Task.FromResult((validation, address));
        }
    }
}
