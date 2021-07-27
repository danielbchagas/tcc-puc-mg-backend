using System;

namespace ECommerce.Clientes.Domain.Models
{
    public class Endereco : Entity
    {
        public Endereco()
        {

        }

        public Endereco(Guid id, string logradouro, string bairro, string cidade, string cep, Estados estado)
        {
            Id = id;
            Logradouro = logradouro;
            Bairro = bairro;
            Cidade = cidade;
            Cep = cep;
            Estados = estado;
        }

        public string Logradouro { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Cep { get; private set; }
        public Estados Estados { get; private set; }

        // Relacionamento
        public Guid ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}