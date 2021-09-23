namespace ECommerce.Pedido.Domain.Models
{
    public class Documento : Entity
    {
        public Documento(string numero)
        {
            Numero = numero;
        }
        
        public string Numero { get; set; }
    }
}
