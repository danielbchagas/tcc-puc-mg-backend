using System;

namespace ECommerce.Pedido.Domain.Models
{
    public class Email : Entity
    {
        #region Construtores
        public Email(string endereco)
        {
            Endereco = endereco;
        }

        public Email(string endereco, Guid clienteId)
        {
            Endereco = endereco;
            ClienteId = clienteId;
        }
        #endregion

        #region Propriedades
        public string Endereco { get; set; }

        public Guid ClienteId { get; set; }

        public Cliente Cliente { get; set; }
        #endregion
    }
}
