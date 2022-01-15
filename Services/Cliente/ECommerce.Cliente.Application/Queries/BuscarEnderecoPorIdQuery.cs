using System;
using ECommerce.Cliente.Domain.Models;
using MediatR;

namespace ECommerce.Cliente.Application.Queries
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
