using System;

namespace ECommerce.Clientes.Domain.Models
{
    public class Endereco : Entity
    {
        protected Endereco()
        {

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

        public string Logradouro { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Cep { get; private set; }
        public Estados Estado { get; private set; }

        // Relacionamento
        public Guid ClienteId { get; private set; }
        public Cliente Cliente { get; private set; }
    }
}