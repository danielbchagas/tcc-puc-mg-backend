using System;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Carts.Application.Queries;
using ECommerce.Carts.Domain.Models;
using MediatR;

namespace ECommerce.Carts.Application.Handlers.Queries
{
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, Cart>
    {
        public Task<Cart> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
