using ECommerce.Clientes.Domain.Application.Notifications;
using ECommerce.Clientes.Domain.Interfaces.Repositories;
using ECommerce.Clientes.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Application.Handlers.Notifications
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
            _repository.Adicionar(new LogEvento(clienteId: notification.ClienteId, usuarioId: notification.UsuarioId));
            _repository.UnitOfWork.Commit();

            return Task.CompletedTask;
        }
    }
}
