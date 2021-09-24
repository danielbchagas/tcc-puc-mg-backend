namespace ECommerce.Pedido.Domain.Models
{
    public class Email : Entity
    {
        #region Construtores
        public Email(string endereco)
        {
            Endereco = endereco;
        }
        #endregion

        #region Propriedades
        public string Endereco { get; set; }
        #endregion
    }
}
