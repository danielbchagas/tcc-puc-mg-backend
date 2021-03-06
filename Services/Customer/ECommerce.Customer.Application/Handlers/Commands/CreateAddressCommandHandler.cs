using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Customer.Application.Commands;
using ECommerce.Customer.Domain.Interfaces.Repositories;
using ECommerce.Customer.Domain.Models;
using FluentValidation.Results;
using MediatR;

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

            var addresses = await _repository.Get(d => d.UserId == request.UserId);

            if (addresses.Any())
            {
                validation.Errors.Add(new ValidationFailure("", "O cliente já possui um endereço cadastrado."));

                return validation;
            }

            var address = new Address(request.FirstLine, request.SecondLine, request.City, request.ZipCode, request.State, request.UserId);

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
