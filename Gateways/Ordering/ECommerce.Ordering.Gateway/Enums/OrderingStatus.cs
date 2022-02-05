using System.ComponentModel;

namespace ECommerce.Ordering.Gateway.Enums
{
    public enum OrderingStatus
    {
        [Description("Processando")]
        Processando,
        [Description("Finalizado")]
        Finalizado,
        [Description("Cancelado")]
        Cancelado
    }
}
