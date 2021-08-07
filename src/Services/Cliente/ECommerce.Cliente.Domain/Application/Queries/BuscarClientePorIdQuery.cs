using System;
using MediatR;

namespace ECommerce.Cliente.Domain.Application.Queries
{
    public class BuscarClientePorIdQuery : IRequest<Models.Cliente>
    {
        public BuscarClientePorIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
