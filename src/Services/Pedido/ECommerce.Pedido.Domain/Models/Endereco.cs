using ECommerce.Pedido.Domain.Enums;
using System;

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

        public Endereco(string logradouro, string bairro, string cidade, string cep, Estados estado, Guid clienteId)
        {
            Logradouro = logradouro;
            Bairro = bairro;
            Cidade = cidade;
            Cep = cep;
            Estado = estado;
            ClienteId = clienteId;
        }
        #endregion

        #region Propriedades
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public Estados Estado { get; set; }

        public Guid ClienteId { get; set; }

        public Cliente Cliente { get; set; }
        #endregion
    }
}
