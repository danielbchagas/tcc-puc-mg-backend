namespace ECommerce.Pedido.Domain.Models
{
    public class Cliente : Entity
    {
        #region Construtores
        public Cliente(string nome, string sobrenome)
        {
            Nome = nome;
            Sobrenome = sobrenome;
        }

        public Cliente(string nome, string sobrenome, Documento documento, Email email, Telefone telefone, Endereco endereco)
        {
            Nome = nome;
            Sobrenome = sobrenome;

            Documento = documento;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
        }
        #endregion

        #region Propriedades
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        public Documento Documento { get; set; }
        public Email Email { get; set; }
        public Telefone Telefone { get; set; }
        public Endereco Endereco { get; set; }
        #endregion
    }
}
