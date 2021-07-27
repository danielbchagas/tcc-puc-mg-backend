using Dominio = ECommerce.Clientes.Domain.Models;
using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Queries.Endereco
{
    public class BuscarEnderecoPorIdQuery : IRequest<Dominio.Endereco>
    {
        public BuscarEnderecoPorIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
