﻿using ECommerce.Customers.Application.Commands.Customer;
using ECommerce.Customers.Domain.Interfaces.Data;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customers.Application.Handlers.Commands.Customer
{
    public class DisableCustomerCommandHandler : IRequestHandler<DisableCustomerCommand, ValidationResult>
    {
        public DisableCustomerCommandHandler(ICustomerRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        private readonly ICustomerRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public async Task<ValidationResult> Handle(DisableCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.Get(request.Id);

            if (customer != null)
            {
                var validation = customer.Validate();

                if (validation.IsValid)
                {
                    customer.DeletedAt = DateTime.UtcNow;

                    await _unitOfWork.Commit();
                }

                return await Task.FromResult(validation);
            }

            return await Task.FromResult(new ValidationResult());
        }
    }
}
