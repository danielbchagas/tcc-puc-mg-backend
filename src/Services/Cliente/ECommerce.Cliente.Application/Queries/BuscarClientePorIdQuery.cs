using System;
using MediatR;

namespace ECommerce.Cliente.Application.Queries
{
    public class BuscarClientePorIdQuery : IRequest<Domain.Models.Cliente>
    {
        public BuscarClientePorIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
