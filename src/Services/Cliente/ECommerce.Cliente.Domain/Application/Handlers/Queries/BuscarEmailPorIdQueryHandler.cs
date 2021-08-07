using System.Threading;
using System.Threading.Tasks;
using ECommerce.Cliente.Domain.Application.Queries;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using MediatR;

namespace ECommerce.Cliente.Domain.Application.Handlers.Queries
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
