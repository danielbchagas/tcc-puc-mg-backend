using System;
using ECommerce.Cliente.Domain.Models;
using MediatR;

namespace ECommerce.Cliente.Application.Queries
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
