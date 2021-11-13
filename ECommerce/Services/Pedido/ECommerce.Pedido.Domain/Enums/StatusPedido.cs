using System.ComponentModel;

namespace ECommerce.Pedido.Domain.Enums
{
    public enum StatusPedido
    {
        [Description("Processando")]
        Processando,
        [Description("Finalizado")]
        Finalizado,
        [Description("Cancelado")]
        Cancelado
    }
}
