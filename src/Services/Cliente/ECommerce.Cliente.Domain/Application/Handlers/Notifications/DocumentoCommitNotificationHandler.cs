using System.Threading;
using System.Threading.Tasks;
using ECommerce.Cliente.Domain.Application.Notifications;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using MediatR;

namespace ECommerce.Cliente.Domain.Application.Handlers.Notifications
{
    public class DocumentoCommitNotificationHandler : INotificationHandler<DocumentoCommitNotification>
    {
        public DocumentoCommitNotificationHandler(ILogEventoRepository repository)
        {
            _repository = repository;
        }

        private readonly ILogEventoRepository _repository;

        public Task Handle(DocumentoCommitNotification notification, CancellationToken cancellationToken)
        {
            _repository.Adicionar(new LogEvento(entidadeId: notification.DocumentoId, usuarioId: notification.UsuarioId));
            _repository.UnitOfWork.Commit();

            return Task.CompletedTask;
        }
    }
}
