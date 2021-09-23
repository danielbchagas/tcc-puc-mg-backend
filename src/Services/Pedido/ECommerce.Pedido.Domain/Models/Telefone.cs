namespace ECommerce.Pedido.Domain.Models
{
    public class Telefone : Entity
    {
        public Telefone(string numero)
        {
            Numero = numero;
        }

        public string Numero { get; set; }
    }
}
