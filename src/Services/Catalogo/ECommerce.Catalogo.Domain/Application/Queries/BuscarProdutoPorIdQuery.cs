using System;
using ECommerce.Catalogo.Domain.Models;
using MediatR;

namespace ECommerce.Catalogo.Domain.Application.Queries
{
    public class BuscarProdutoPorIdQuery : IRequest<Produto>
    {
        public BuscarProdutoPorIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
