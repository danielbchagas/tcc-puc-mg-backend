using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Application.Queries;
using ECommerce.Basket.Api.Protos;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Basket.Api.Services.gRPC
{
    [Authorize]
    public class BasketGrpcService : ShoppingBasketService.ShoppingBasketServiceBase
    {
        private readonly IMediator _mediator;

        public BasketGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<CreateBasketResponse> CreateBasket(CreateBasketRequest request, ServerCallContext context)
        {
            var createCustomerBasketCommand = new CreateShoppingBasketCommand(
                id: Guid.Parse(request.Id),
                customerId: Guid.Parse(request.Customerid)
            );

            var result = await _mediator.Send(createCustomerBasketCommand);

            return new CreateBasketResponse()
            {
                Isvalid = result.IsValid,
                Message = JsonSerializer.Serialize(result.Errors)
            };
        }

        public override async Task<GetBasketResponse> GetBasketByCustomer(GetBasketByCustomerRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetShoppingBasketByCustomerQuery(Guid.Parse(request.Customerid)));

            if(result == null)
            {
                return new GetBasketResponse
                {
                    Basket = null
                };
            }

            var response = new GetBasketResponse
            {
                Basket = new ShoppingBasket
                {
                    Id = Convert.ToString(result.Id),
                    Customerid = Convert.ToString(result.CustomerId),
                    Value = Convert.ToDouble(result.Value),
                }
            };

            foreach(var item in result.Items)
            {
                response.Basket.Items.Add(new BasketItem
                {
                    Id = Convert.ToString(item.Id),
                    Name = item.Name,
                    Image = item.Image,
                    Quantity = item.Quantity,
                    Value = Convert.ToDouble(item.Value),
                    Shoppingbasketid = Convert.ToString(item.ShoppingBasketId)
                });
            }

            return response;
        }

        public override async Task<DeleteBasketResponse> DeleteBasket(DeleteBasketRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new DeleteShoppingBasketCommand(Guid.Parse(request.Id)));

            return new DeleteBasketResponse
            {
                Isvalid = result.IsValid,
                Message = JsonSerializer.Serialize(result.Errors)
            };
        }
    }
}
