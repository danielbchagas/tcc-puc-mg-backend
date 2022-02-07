using System.ComponentModel;

namespace ECommerce.Core.Enums.Ordering
{
    public enum OrderStatus
    {
        [Description("Processando")]
        Processando,
        [Description("Finalizado")]
        Finalizado,
        [Description("Cancelado")]
        Cancelado
    }
}
