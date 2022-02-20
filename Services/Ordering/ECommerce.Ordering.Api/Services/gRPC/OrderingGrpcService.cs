using ECommerce.Ordering.Api.Protos;
using ECommerce.Ordering.Application.Commands;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Api.Services.gRPC
{
    [Authorize]
    public class OrderingGrpcService : ECommerce.Ordering.Api.Protos.OrderingService.OrderingServiceBase
    {
        private readonly IMediator _mediator;

        public OrderingGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request, ServerCallContext context)
        {
            var createOrderCommand = new CreateOrderCommand(
                id: Guid.Parse(request.Id),
                fullName: request.Fullname,
                document: request.Document,
                phone: request.Phone,
                email: request.Email,
                value: Convert.ToDecimal(request.Value),
                firstLine: request.Firstline,
                secondLine: request.Secondline,
                city: request.City,
                state: request.State,
                zipCode: request.Zipcode
            );

            foreach (var item in request.Items)
            {
                createOrderCommand.Items.Add(new Domain.Models.OrderItem(
                    id: Guid.Parse(item.Id),
                    name: item.Name,
                    quantity: item.Quantity,
                    value: Convert.ToDecimal(item.Value),
                    image: item.Image,
                    productId: Guid.Parse(item.Productid),
                    orderId: Guid.Parse(item.Orderid)
                ));
            }

            var result = await _mediator.Send(createOrderCommand);

            return new CreateOrderResponse
            {
                Isvalid = result.IsValid,
                Message = JsonSerializer.Serialize(result.Errors)
            };
        }
    }
}
