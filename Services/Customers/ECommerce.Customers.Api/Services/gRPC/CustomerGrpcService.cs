using AutoMapper;
using ECommerce.Customer.Api.Services.gRPC.Validators;
using ECommerce.Customers.Application.Commands.Customer;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        public override async Task<Customers.Api.Protos.CreateCustomerResponse> CreateCustomer(Customers.Api.Protos.CreateCustomerRequest request, ServerCallContext context)
        {
            var validation = await new CustomerGrpcValidator().ValidateAsync(request);

            if (!validation.IsValid)
            {
                return new Customers.Api.Protos.CreateCustomerResponse()
                {
                    Isvalid = false,
                    Message = JsonSerializer.Serialize(validation.Errors)
                };
            }

            var result = await _mediator.Send(_mapper.Map<CreateCustomerCommand>(request));

            return new Customers.Api.Protos.CreateCustomerResponse()
            {
                Isvalid = result.Item1.IsValid,
                Message = JsonSerializer.Serialize(result.Item1.Errors),
                Customer = _mapper.Map<Customers.Api.Protos.Customer>(result.Item2)
            };
        }
    }
}
