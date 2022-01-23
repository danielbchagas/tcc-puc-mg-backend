using System;
using MediatR;

namespace ECommerce.Cliente.Application.Queries
{
    public class GetClienteQuery : IRequest<Domain.Models.Cliente>
    {
        public GetClienteQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
