using ECommerce.Clientes.Domain.Models;
using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Queries
{
    public class BuscarClientePorIdQuery : IRequest<Cliente>
    {
        public BuscarClientePorIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
