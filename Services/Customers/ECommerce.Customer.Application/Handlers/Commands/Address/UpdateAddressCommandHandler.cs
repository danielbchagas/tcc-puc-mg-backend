﻿using ECommerce.Customers.Application.Commands.Address;
using ECommerce.Customers.Domain.Interfaces.Data;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customers.Application.Handlers.Commands.Address
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, ValidationResult>
    {
        public UpdateAddressCommandHandler(IAddressRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        private readonly IAddressRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

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
                await _unitOfWork.Commit();
            }

            return await Task.FromResult(validation);
        }
    }
}
