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
    public class CustomerGrpcService : Protos.CustomerService.CustomerServiceBase
    {
        private readonly IMediator _mediator;

        public CustomerGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<Protos.CreateUserResponse> CreateCustomer(Protos.CreateUserRequest request, ServerCallContext context)
        {
            var document = new Domain.Models.Document(
                number: request.Document.Number,
                userId: Guid.Parse(request.Document.Userid)
            );

            var email = new Domain.Models.Email(
                address: request.Email.Address,
                userId: Guid.Parse(request.Email.Userid)
            );

            var phone = new Domain.Models.Phone(
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

            return new Protos.CreateUserResponse()
            {
                Isvalid = result.IsValid,
                Message = JsonSerializer.Serialize(result.Errors)
            };
        }

        public override async Task<Protos.GetUserResponse> GetCustomer(Protos.GetUserRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetUserQuery(Guid.Parse(request.Id)));

            if(result == null)
            {
                return new Protos.GetUserResponse
                {
                    User = null
                };
            }

            return new Protos.GetUserResponse
            {
                User = new Protos.User
                {
                    Id = Convert.ToString(result.Id),
                    Firstname = result.FirstName,
                    Lastname = result.LastName,
                    Enabled = result.Enabled,
                    Document = result.Document != null ? new Protos.Document
                    {
                        Id = Convert.ToString(result.Document.Id),
                        Number = result.Document.Number,
                        Userid = Convert.ToString(result.Document.UserId)
                    } : null,
                    Email = result.Email != null ? new Protos.Email
                    {
                        Id = Convert.ToString(result.Email.Id),
                        Address = result.Email.Address,
                        Userid = Convert.ToString(result.Email.UserId)
                    } : null,
                    Phone = result.Phone != null ? new Protos.Phone
                    {
                        Id = Convert.ToString(result.Phone.Id),
                        Number= result.Phone.Number,
                        Userid = Convert.ToString(result.Phone.UserId)
                    } : null,
                    Address = result.Address != null ? new Protos.Address
                    {
                        Id = Convert.ToString(result.Address.Id),
                        Firstline = result.Address.FirstLine,
                        Secondline = result.Address.SecondLine,
                        City = result.Address.City,
                        State = result.Address.State,
                        Zipcode = result.Address.ZipCode,
                        Userid = Convert.ToString(result.Address.UserId)
                    } : null
                }
            };
        }
    }
}
