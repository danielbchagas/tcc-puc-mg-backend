using ECommerce.Customer.Api.Protos;
using ECommerce.Customer.Application.Commands;
using ECommerce.Customer.Application.Queries;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Text.Json;
using System.Threading.Tasks;

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

        public override async Task<GetUserResponse> GetCustomer(GetUserRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetUserQuery(Guid.Parse(request.Id)));

            if(result == null)
            {
                return new GetUserResponse
                {
                    User = null
                };
            }

            return new GetUserResponse
            {
                User = new User
                {
                    Id = Convert.ToString(result.Id),
                    Firstname = result.FirstName,
                    Lastname = result.LastName,
                    Enabled = result.Enabled,
                    Document = new Document
                    {
                        Id = Convert.ToString(result.Document.Id),
                        Number = result.Document.Number,
                        Userid = Convert.ToString(result.Document.UserId)
                    },
                    Email = new Email
                    {
                        Id = Convert.ToString(result.Email.Id),
                        Address = result.Email.Address,
                        Userid = Convert.ToString(result.Email.UserId)
                    },
                    Phone = new Phone
                    {
                        Id = Convert.ToString(result.Phone.Id),
                        Number= result.Phone.Number,
                        Userid = Convert.ToString(result.Phone.UserId)
                    },
                    Address = new Address
                    {
                        Id = Convert.ToString(result.Address.Id),
                        Firstline = result.Address.FirstLine,
                        Secondline = result.Address.SecondLine,
                        City = result.Address.City,
                        State = result.Address.State,
                        Zipcode = result.Address.ZipCode,
                        Userid = Convert.ToString(result.Address.UserId)
                    }
                }
            };
        }
    }
}
