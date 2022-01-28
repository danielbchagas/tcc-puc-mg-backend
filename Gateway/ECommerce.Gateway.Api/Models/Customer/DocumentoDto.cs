using System;

namespace ECommerce.Gateway.Api.Models.Cliente
{
    public class DocumentoDto
    {
        public Guid Id { get; set; }
        public string Numero { get; set; }

        public Guid ClienteId { get; set; }
    }
}
