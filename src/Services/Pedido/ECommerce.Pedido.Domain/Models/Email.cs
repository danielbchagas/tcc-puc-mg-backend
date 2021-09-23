namespace ECommerce.Pedido.Domain.Models
{
    public class Email : Entity
    {
        public Email(string endereco)
        {
            Endereco = endereco;
        }
        
        public string Endereco { get; set; }
    }
}
