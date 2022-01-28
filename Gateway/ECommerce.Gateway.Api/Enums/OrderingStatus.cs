using System.ComponentModel;

namespace ECommerce.Gateway.Api.Enums
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
