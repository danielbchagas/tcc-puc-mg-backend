using System;

namespace ECommerce.Pedido.Domain.Models
{
    public class Documento : Entity
    {
        #region Construtores
        public Documento(string numero)
        {
            Numero = numero;
        }

        public Documento(string numero, Guid clienteId)
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
