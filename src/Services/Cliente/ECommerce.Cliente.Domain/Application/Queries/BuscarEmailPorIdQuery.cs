using System;
using ECommerce.Cliente.Domain.Models;
using MediatR;

namespace ECommerce.Cliente.Domain.Application.Queries
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
