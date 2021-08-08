using System;
using ECommerce.Cliente.Domain.Models;
using MediatR;

namespace ECommerce.Cliente.Domain.Application.Queries
{
    public class BuscarTelefonePorIdQuery : IRequest<Telefone>
    {
        public BuscarTelefonePorIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
