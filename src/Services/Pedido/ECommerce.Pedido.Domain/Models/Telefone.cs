using System;

namespace ECommerce.Pedido.Domain.Models
{
    public class Telefone : Entity
    {
        #region Construtores
        public Telefone(string numero)
        {
            Numero = numero;
        }

        public Telefone(string numero, Guid clienteId)
        {
            Numero = numero;

            ClienteId = clienteId;
        }
        #endregion

        #region Propriedades
        public string Numero { get; set; }

        public Guid ClienteId { get; set; }

        public Cliente Cliente { get; set; }
        #endregion
    }
}
