using AutoMapper;
using ECommerce.Customer.Api.Services.gRPC.Validators;
using ECommerce.Customers.Application.Commands.User;
using ECommerce.Customers.Application.Queries;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Customer.Api.Services.gRPC
{
    [Authorize]
    public class CustomerGrpcService : Customers.Api.Protos.CustomerService.CustomerServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CustomerGrpcService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<Customers.Api.Protos.CreateUserResponse> CreateCustomer(Customers.Api.Protos.CreateUserRequest request, ServerCallContext context)
        {
            var validation = await new CustomerGrpcValidator().ValidateAsync(request);

            if (!validation.IsValid)
            {
                return new Customers.Api.Protos.CreateUserResponse()
                {
                    Isvalid = false,
                    Message = JsonSerializer.Serialize(validation.Errors)
                };
            }

            var result = await _mediator.Send(_mapper.Map<CreateCustomerCommand>(request));

            return new Customers.Api.Protos.CreateUserResponse()
            {
                Isvalid = result.IsValid,
                Message = JsonSerializer.Serialize(result.Errors)
            };
        }

        public override async Task<Customers.Api.Protos.GetUserResponse> GetCustomer(Customers.Api.Protos.GetUserRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetUserQuery(Guid.Parse(request.Id)));

            var valid = result.Validate();

            if (!valid.IsValid)
            {
                return new Customers.Api.Protos.GetUserResponse
                {
                    User = null
                };
            }

            return new Customers.Api.Protos.GetUserResponse
            {
                User = new Customers.Api.Protos.User
                {
                    Id = Convert.ToString(result.Id),
                    Firstname = result.FirstName,
                    Lastname = result.LastName,
                    Document = new Customers.Api.Protos.Document
                    {
                        Id = Convert.ToString(result.Document.Id),
                        Number = result.Document.Number,
                        Userid = Convert.ToString(result.Document.CustomerId)
                    },
                    Email = new Customers.Api.Protos.Email
                    {
                        Id = Convert.ToString(result.Email.Id),
                        Address = result.Email.Address,
                        Userid = Convert.ToString(result.Email.CustomerId)
                    },
                    Phone = new Customers.Api.Protos.Phone
                    {
                        Id = Convert.ToString(result.Phone.Id),
                        Number = result.Phone.Number,
                        Userid = Convert.ToString(result.Phone.CustomerId)
                    },
                    Address = new Customers.Api.Protos.Address
                    {
                        Id = Convert.ToString(result.Address.Id),
                        Firstline = result.Address.FirstLine,
                        Secondline = result.Address.SecondLine,
                        City = result.Address.City,
                        State = result.Address.State,
                        Zipcode = result.Address.ZipCode,
                        Userid = Convert.ToString(result.Address.CustomerId)
                    }
                }
            };
        }
    }
}
