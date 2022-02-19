using ECommerce.Customer.Api.Protos;
using ECommerce.Customer.Application.Commands;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace ECommerce.Customer.Api.Services.gRPC
{
    [Authorize]
    public class CustomerGrpcService : ECommerce.Customer.Api.Protos.Customer.CustomerBase
    {
        private readonly IMediator _mediator;

        public CustomerGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<ECommerce.Customer.Api.Protos.CreateUserResponse> CreateCustomer(ECommerce.Customer.Api.Protos.CreateUserRequest request, ServerCallContext context)
        {
            var document = new ECommerce.Customer.Domain.Models.Document(
                number: request.Document.Number,
                userId: Guid.Parse(request.Document.Userid)
            );

            var email = new ECommerce.Customer.Domain.Models.Email(
                address: request.Email.Address,
                userId: Guid.Parse(request.Email.Userid)
            );

            var phone = new ECommerce.Customer.Domain.Models.Phone(
                number: request.Phone.Number,
                userId: Guid.Parse(request.Phone.Userid)
            );

            var createUserCommand = new CreateUserCommand(
                id: Guid.Parse(request.Id),
                firstName: request.Firstname,
                lastName: request.Lastname,
                enabled: true,
                document: document,
                email: email,
                phone: phone
            );

            var result = await _mediator.Send(createUserCommand);

            return new CreateUserResponse()
            {
                Isvalid = result.IsValid,
                Message = JsonSerializer.Serialize(result.Errors)
            };
        }
    }
}
