namespace ECommerce.Pedido.Domain.Models
{
    public class Documento : Entity
    {
        #region Construtores
        public Documento(string numero)
        {
            Numero = numero;
        }
        #endregion

        #region Propriedades
        public string Numero { get; set; }
        #endregion
    }
}
