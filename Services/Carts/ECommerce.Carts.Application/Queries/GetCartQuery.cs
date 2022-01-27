using System;
using ECommerce.Carts.Domain.Models;
using MediatR;

namespace ECommerce.Carts.Application.Queries
{
    public class GetCartQuery : IRequest<Cart>
    {
        public GetCartQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
