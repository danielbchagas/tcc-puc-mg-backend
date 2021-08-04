using ECommerce.Clientes.Domain.Models;
using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Queries
{
    public class BuscarEnderecoPorIdQuery : IRequest<Endereco>
    {
        public BuscarEnderecoPorIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
