using System.Collections.Generic;
using ECommerce.Catalogo.Domain.Models;
using MediatR;

namespace ECommerce.Catalogo.Domain.Application.Queries
{
    public class BuscarProdutosPaginadosQuery : IRequest<IEnumerable<Produto>>
    {
        public BuscarProdutosPaginadosQuery(int? pagina, int? linhas)
        {
            Pagina = pagina;
            Linhas = linhas;
        }

        public int? Pagina { get; set; }
        public int? Linhas { get; set; }
    }
}
