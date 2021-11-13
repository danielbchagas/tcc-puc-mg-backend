using ECommerce.Cliente.Application.Queries;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Application.Handlers.Queries
{
    public class BuscarEmailPorIdQueryHandler : IRequestHandler<BuscarEmailPorIdQuery, Email>
    {
        public BuscarEmailPorIdQueryHandler(IEmailRepository repository)
        {
            _repository = repository;
        }

        private readonly IEmailRepository _repository;

        public async Task<Email> Handle(BuscarEmailPorIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar(request.Id);
        }
    }
}
