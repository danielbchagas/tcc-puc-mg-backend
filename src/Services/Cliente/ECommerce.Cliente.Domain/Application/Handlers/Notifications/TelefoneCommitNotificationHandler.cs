using ECommerce.Cliente.Domain.Application.Notifications;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Domain.Application.Handlers.Notifications
{
    public class TelefoneCommitNotificationHandler : INotificationHandler<TelefoneCommitNotification>
    {
        public TelefoneCommitNotificationHandler(ILogEventoRepository repository)
        {
            _repository = repository;
        }

        private readonly ILogEventoRepository _repository;

        public Task Handle(TelefoneCommitNotification notification, CancellationToken cancellationToken)
        {
            _repository.Adicionar(new LogEvento(entidadeId: notification.TelefoneId, usuarioId: notification.UsuarioId));
            _repository.UnitOfWork.Commit();

            return Task.CompletedTask;
        }
    }
}
