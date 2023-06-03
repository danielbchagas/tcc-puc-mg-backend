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
            var createCustomerBasketCommand = new CreateBasketCommand(
                id: Guid.Parse(request.Id),
                customerId: Guid.Parse(request.Customerid)
            );

            var result = await _mediator.Send(createCustomerBasketCommand);

            return new CreateBasketResponse()
            {
                Isvalid = result.Item1.IsValid,
                Message = JsonSerializer.Serialize(result.Item1.Errors)
            };
        }

        public override async Task<GetBasketByCustomerResponse> GetBasketByCustomer(GetBasketByCustomerRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetBasketByCustomerQuery(Guid.Parse(request.Customerid)));

            if(result == null)
            {
                return new GetBasketByCustomerResponse
                {
                    Basket = null
                };
            }

            var response = new GetBasketByCustomerResponse
            {
                Basket = new ShoppingBasket
                {
                    Id = Convert.ToString(result.Id),
                    Customerid = Convert.ToString(result.CustomerId),
                    Value = Convert.ToDouble(result.Value),
                    Isended = result.IsEnded
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
                    Productid = Convert.ToString(item.ProductId)
                });
            }

            return response;
        }

        public override async Task<DeleteBasketResponse> DeleteBasket(DeleteBasketRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new DisableBasketCommand(Guid.Parse(request.Id)));

            return new DeleteBasketResponse
            {
                Isvalid = result.IsValid,
                Message = JsonSerializer.Serialize(result.Errors)
            };
        }

        public override async Task<UpdateBasketResponse> UpdateBasket(UpdateBasketRequest request, ServerCallContext context)
        {
            var createCustomerBasketCommand = new UpdateBasketCommand(id: Guid.Parse(request.Id));

            var result = await _mediator.Send(createCustomerBasketCommand);

            return new UpdateBasketResponse()
            {
                Isvalid = result.Item1.IsValid,
                Message = JsonSerializer.Serialize(result.Item1.Errors)
            };
        }
    }
}
