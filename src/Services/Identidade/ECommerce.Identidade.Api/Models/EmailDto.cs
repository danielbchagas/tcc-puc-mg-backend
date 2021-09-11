using System;

namespace ECommerce.Identidade.Api.Models
{
    public class EmailDto
    {
        public Guid Id { get; set; }
        public string Endereco { get; set; }

        public Guid ClienteId { get; set; }
    }
}
