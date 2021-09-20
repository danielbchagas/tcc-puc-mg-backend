using System;

namespace ECommerce.Carrinho.Application.Notifications
{
    public class ItemCarrinhoCommitNotification
    {
        public ItemCarrinhoCommitNotification(Guid itemCarrinhoId, Guid usuarioId)
        {
            Momento = DateTime.Now;
            ItemCarrinhoId = itemCarrinhoId;
            UsuarioId = usuarioId;
        }

        public DateTime Momento { get; private set; }
        public Guid ItemCarrinhoId { get; private set; }
        public Guid UsuarioId { get; private set; }
    }
}
