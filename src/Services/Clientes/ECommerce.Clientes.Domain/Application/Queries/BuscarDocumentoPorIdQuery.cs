using ECommerce.Clientes.Domain.Models;
using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Queries
{
    public class BuscarDocumentoPorIdQuery : IRequest<Documento>
    {
        public BuscarDocumentoPorIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
