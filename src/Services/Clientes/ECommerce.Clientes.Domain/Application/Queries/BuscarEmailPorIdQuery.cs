using ECommerce.Clientes.Domain.Models;
using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Queries
{
    public class BuscarEmailPorIdQuery : IRequest<Email>
    {
        public BuscarEmailPorIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
