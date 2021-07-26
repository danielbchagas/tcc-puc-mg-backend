namespace ECommerce.Clientes.Domain.Models
{
    public class Endereco : Entity
    {
        public string Logradouro { get; private set; }
        public string Bairro { get; set; }
        public string Cidade { get; private set; }
        public string Cep { get; private set; }
        public Estados Estados { get; }
    }
}