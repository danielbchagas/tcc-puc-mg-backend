using ECommerce.Pedido.Domain.Enums;

namespace ECommerce.Pedido.Domain.Models
{
    public class Endereco : Entity
    {
        #region Construtores
        public Endereco(string logradouro, string bairro, string cidade, string cep, Estados estado)
        {
            Logradouro = logradouro;
            Bairro = bairro;
            Cidade = cidade;
            Cep = cep;
            Estado = estado;
        }
        #endregion

        #region Propriedades
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public Estados Estado { get; set; }
        #endregion
    }
}
