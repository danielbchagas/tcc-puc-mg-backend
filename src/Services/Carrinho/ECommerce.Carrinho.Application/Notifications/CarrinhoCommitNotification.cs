using MediatR;
using System;

namespace ECommerce.Carrinho.Application.Notifications
{
    public class CarrinhoCommitNotification : INotification
    {
        public CarrinhoCommitNotification(Guid carrinhoId, Guid clienteId)
        {
            Momento = DateTime.Now;
            CarrinhoId = carrinhoId;
            UsuarioId = clienteId;
        }

        public DateTime Momento { get; private set; }
        public Guid CarrinhoId { get; private set; }
        public Guid UsuarioId { get; private set; }
    }
}
