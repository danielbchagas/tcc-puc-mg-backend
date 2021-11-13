using System.Collections.Generic;
using MediatR;

namespace ECommerce.Cliente.Application.Queries
{
    public class BuscarClientesPaginadosQuery : IRequest<IEnumerable<Domain.Models.Cliente>>
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
