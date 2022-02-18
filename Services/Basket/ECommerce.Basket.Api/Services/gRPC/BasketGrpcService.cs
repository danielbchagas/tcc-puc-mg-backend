using ECommerce.Basket.Application.Queries;
using ECommerce.Catalog.Api.Protos;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace ECommerce.Basket.Api.Services.gRPC
{
    [Authorize]
    public class BasketGrpcService : CustomerBasket.CustomerBasketBase
    {
        private readonly IMediator _mediator;

        public BasketGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<GetBasketResponse> GetBasket(GetBasketRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetCustomerBasketByCustomerQuery(Guid.Parse(request.Customerid)));

            var proto = new GetBasketResponse
            {
                Id = Convert.ToString(result.Id),
                Customerid = Convert.ToString(result.CustomerId),
                Value = Convert.ToDouble(result.Value),
            };

            foreach(var item in result.Items)
            {
                proto.Items.Add(new BasketItemResponse
                {
                    Id = Convert.ToString(item.Id),
                    Name = item.Name,
                    Image = item.Image,
                    Quantity = item.Quantity,
                    Value = Convert.ToDouble(item.Value),
                    Basketid = Convert.ToString(item.CustomerBasketId)
                });
            }

            return proto;
        }
    }
}
