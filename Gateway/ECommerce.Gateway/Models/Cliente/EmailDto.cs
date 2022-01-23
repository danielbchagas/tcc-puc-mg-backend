using System;

namespace ECommerce.Compras.Gateway.Models.Cliente
{
    public class EmailDto
    {
        public Guid Id { get; set; }
        public string Endereco { get; set; }

        public Guid ClienteId { get; set; }
    }
}
