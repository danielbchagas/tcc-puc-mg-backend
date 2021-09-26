using System.ComponentModel;

namespace ECommerce.Compras.Gateway.Enums
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
