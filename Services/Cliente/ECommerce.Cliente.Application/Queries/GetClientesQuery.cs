using System.Collections.Generic;
using MediatR;

namespace ECommerce.Cliente.Application.Queries
{
    public class GetClientesQuery : IRequest<IEnumerable<Domain.Models.Cliente>>
    {
        public GetClientesQuery(int pagina, int linhas)
        {
            Pagina = pagina;
            Linhas = linhas;
        }

        public int Pagina { get; set; }
        public int Linhas { get; set; }
    }
}
