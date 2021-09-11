using System;

namespace ECommerce.Identidade.Api.Models
{
    public class DocumentoDto
    {
        public string Numero { get; set; }
        public Guid ClienteId { get; set; }
    }
}
