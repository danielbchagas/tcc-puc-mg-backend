using MediatR;
using System;

namespace ECommerce.Products.Application.Queries
{
    public class GetImageQuery : IRequest<(Guid, string)>
    {
        public GetImageQuery(Guid productId)
        {
            ProductId = productId;
        }

        public Guid ProductId { get; set; }
    }
}
