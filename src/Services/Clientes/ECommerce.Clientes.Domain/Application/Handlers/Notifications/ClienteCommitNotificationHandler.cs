using ECommerce.Clientes.Domain.Application.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Application.Handlers.Notifications
{
    public class ClienteCommitNotificationHandler : INotificationHandler<ClienteCommitNotification>
    {
        public Task Handle(ClienteCommitNotification notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
