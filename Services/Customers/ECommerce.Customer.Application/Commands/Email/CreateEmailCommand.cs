﻿using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customers.Application.Commands.Email
{
    public class CreateEmailCommand : IRequest<ValidationResult>
    {
        public CreateEmailCommand(Guid id, string address, Guid userId)
        {
            Id = id;
            Address = address;
            UserId = userId;
        }

        public Guid Id { get; set; }
        public string Address { get; set; }
        public Guid UserId { get; set; }
    }
}