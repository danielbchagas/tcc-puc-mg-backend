using MediatR;
using System;

namespace ECommerce.Carrinho.Application.Notifications
{
    public class CarrinhoCommitNotification : INotification
    {
        public CarrinhoCommitNotification(Guid carrinhoId, Guid usuarioId)
        {
            Momento = DateTime.Now;
            CarrinhoId = carrinhoId;
            UsuarioId = usuarioId;
        }

        public DateTime Momento { get; private set; }
        public Guid CarrinhoId { get; private set; }
        public Guid UsuarioId { get; private set; }
    }
}
