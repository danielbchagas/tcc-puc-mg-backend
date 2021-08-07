using System.ComponentModel;

namespace ECommerce.Catalogo.Domain.Enums
{
    public enum ErrosValidacao
    {
        [Description("{PropertyName} não pode ser nulo ou vazio")]
        NuloOuVazio,
        [Description("{PropertyName} tem um valor maior do que o esperado!")]
        MaiorQue,
        [Description("{PropertyName} tem um valor menor do que o esperado!")]
        MenorQue
    }
}
