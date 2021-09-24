namespace ECommerce.Pedido.Domain.Models
{
    public class Telefone : Entity
    {
        #region Construtores
        public Telefone(string numero)
        {
            Numero = numero;
        }
        #endregion

        #region Propriedades
        public string Numero { get; set; }
        #endregion
    }
}
