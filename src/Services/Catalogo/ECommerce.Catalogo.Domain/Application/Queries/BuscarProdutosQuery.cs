using System.Collections.Generic;
using ECommerce.Catalogo.Domain.Models;
using MediatR;

namespace ECommerce.Catalogo.Domain.Application.Queries
{
    public class BuscarProdutosQuery : IRequest<IEnumerable<Produto>>
    {
    }
}
