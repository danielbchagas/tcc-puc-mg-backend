using ECommerce.Clientes.Domain.Interfaces.Entities;

namespace ECommerce.Clientes.Domain.Models
{
    public class Cliente : Entity, IAggregateRoot
    {
        protected Cliente()
        {

        }

        public Cliente(string nomeFantasia, bool ativo = true)
        {
            NomeFantasia = nomeFantasia;
            Ativo = ativo;
        }

        public string NomeFantasia { get; private set; }
        public bool Ativo { get; private set; }

        // Relacionamento
        public Documento Documento { get; private set; }
        public Endereco Endereco { get; private set; }

        // Métodos auxiliares
        public void Ativar()
        {
            Ativo = true;
        }

        public void Desativar()
        {
            Ativo = false;
        }

        public void VincularDocumento(Documento documento)
        {
            Documento = documento;
        }

        public void VincularEndereco(Endereco endereco)
        {
            Endereco = endereco;
        }
    }
}
