using ECommerce.Catalogo.Domain.Models;
using MediatR;
using System;

namespace ECommerce.Catalogo.Application.Queries
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
