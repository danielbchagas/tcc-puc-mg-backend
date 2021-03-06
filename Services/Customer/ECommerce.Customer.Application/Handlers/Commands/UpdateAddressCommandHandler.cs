using System.Threading;
using System.Threading.Tasks;
using ECommerce.Customer.Application.Commands;
using ECommerce.Customer.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customer.Application.Handlers.Commands
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, ValidationResult>
    {
        public UpdateAddressCommandHandler(IAddressRepository repository)
        {
            _repository = repository;
        }

        private readonly IAddressRepository _repository;
        
        public async Task<ValidationResult> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await _repository.Get(request.Id);
            address.FirstLine = request.FirstLine;
            address.SecondLine = request.SecondLine;
            address.City = request.City;
            address.ZipCode = request.ZipCode;
            address.State = request.State;

            var validation = address.Validate();

            if (validation.IsValid)
            {
                await _repository.Update(address);
                await _repository.UnitOfWork.Commit();
            }

            return await Task.FromResult(validation);
        }
    }
}
