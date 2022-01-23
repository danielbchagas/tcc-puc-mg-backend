using System;
using ECommerce.Cliente.Domain.Models;
using MediatR;

namespace ECommerce.Cliente.Application.Queries
{
    public class GetDocumentoQuery : IRequest<Documento>
    {
        public GetDocumentoQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
