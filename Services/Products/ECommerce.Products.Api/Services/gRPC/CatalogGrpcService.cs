using ECommerce.Products.Api.Protos;
using ECommerce.Products.Application.Commands;
using ECommerce.Products.Application.Queries;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Products.Api.Services.gRPC
{
    [Authorize]
    public class CatalogGrpcService : ProductsService.ProductsServiceBase
    {
        private readonly IMediator _mediator;

        public CatalogGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<GetProductResponse> GetProduct(GetProductRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetProductQuery(Guid.Parse(request.Id)));

            if (result == null)
            {
                return new GetProductResponse
                {
                    Product = null
                };
            }

            return new GetProductResponse
            {
                Product = new Product
                {
                    Id = Convert.ToString(result.Id),
                    Name = result.Name,
                    Description = result.Description,
                    Image = result.Image,
                    Value = Convert.ToDouble(result.Value),
                    Quantity = result.Quantity
                }
            };
        }

        public override async Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new UpdateProductCommand(
                id: Guid.Parse(request.Id),
                description: request.Description,
                name: request.Name,
                image: request.Image,
                quantity: request.Quantity,
                value: Convert.ToDecimal(request.Value)
            ));

            return new UpdateProductResponse
            {
                Isvalid = result.IsValid,
                Message = JsonSerializer.Serialize(result.Errors)
            };
        }
    }
}
