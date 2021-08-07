using System.Threading;
using System.Threading.Tasks;
using ECommerce.Cliente.Domain.Application.Notifications;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using MediatR;

namespace ECommerce.Cliente.Domain.Application.Handlers.Notifications
{
    public class ClienteCommitNotificationHandler : INotificationHandler<ClienteCommitNotification>
    {
        public ClienteCommitNotificationHandler(ILogEventoRepository repository)
        {
            _repository = repository;
        }

        private readonly ILogEventoRepository _repository;

        public Task Handle(ClienteCommitNotification notification, CancellationToken cancellationToken)
        {
            _repository.Adicionar(new LogEvento(entidadeId: notification.ClienteId, usuarioId: notification.UsuarioId));
            _repository.UnitOfWork.Commit();

            return Task.CompletedTask;
        }
    }
}
