using ECommerce.Catalog.Api.Protos;
using ECommerce.Catalog.Application.Commands;
using ECommerce.Catalog.Application.Queries;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Api.Services.gRPC
{
    [Authorize]
    public class CatalogGrpcService : Catalog.Api.Protos.CatalogService.CatalogServiceBase
    {
        private readonly IMediator _mediator;

        public CatalogGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<GetProductResponse> GetProduct(GetProductRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetProductQuery(Guid.Parse(request.Id)));

            if(result == null)
            {
                return new Catalog.Api.Protos.GetProductResponse
                {
                    Product = null
                };
            }

            return new Catalog.Api.Protos.GetProductResponse
            {
                Product = new Catalog.Api.Protos.Product
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

            return new Catalog.Api.Protos.UpdateProductResponse
            {
                Isvalid = result.IsValid,
                Message = JsonSerializer.Serialize(result.Errors)
            };
        }
    }
}
