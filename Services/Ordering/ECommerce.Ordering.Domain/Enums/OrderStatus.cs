using System.ComponentModel;

namespace ECommerce.Ordering.Domain.Enums
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
