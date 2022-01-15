using System;

namespace ECommerce.Identidade.Api.Models
{
    public class TelefoneDto
    {
        public Guid Id { get; set; }
        public string Numero { get; set; }

        public Guid ClienteId { get; set; }
    }
}
