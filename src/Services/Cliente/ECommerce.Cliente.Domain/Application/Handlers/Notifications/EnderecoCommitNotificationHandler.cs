using System.Threading;
using System.Threading.Tasks;
using ECommerce.Cliente.Domain.Application.Notifications;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using MediatR;

namespace ECommerce.Cliente.Domain.Application.Handlers.Notifications
{
    public class EnderecoCommitNotificationHandler : INotificationHandler<EnderecoCommitNotification>
    {
        public EnderecoCommitNotificationHandler(ILogEventoRepository repository)
        {
            _repository = repository;
        }

        private readonly ILogEventoRepository _repository;

        public Task Handle(EnderecoCommitNotification notification, CancellationToken cancellationToken)
        {
            _repository.Adicionar(new LogEvento(entidadeId: notification.EnderecoId, usuarioId: notification.UsuarioId));
            _repository.UnitOfWork.Commit();

            return Task.CompletedTask;
        }
    }
}
