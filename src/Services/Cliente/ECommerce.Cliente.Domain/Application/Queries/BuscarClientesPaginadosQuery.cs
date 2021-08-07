using System.Collections.Generic;
using MediatR;

namespace ECommerce.Cliente.Domain.Application.Queries
{
    public class BuscarClientesPaginadosQuery : IRequest<IEnumerable<Models.Cliente>>
    {
        public BuscarClientesPaginadosQuery(int pagina, int linhas)
        {
            Pagina = pagina;
            Linhas = linhas;
        }

        public int Pagina { get; set; }
        public int Linhas { get; set; }
    }
}
