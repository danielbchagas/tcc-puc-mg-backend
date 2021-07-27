using Dominio = ECommerce.Clientes.Domain.Models;
using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Queries.Cliente
{
    public class BuscarClientePorIdQuery : IRequest<Dominio.Cliente>
    {
        public BuscarClientePorIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
