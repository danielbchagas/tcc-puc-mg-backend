using System;
using System.Text.Json.Serialization;

namespace ECommerce.Compras.Gateway.Models.Cliente
{
    public class TelefoneDto
    {
        public Guid Id { get; set; }
        public string Numero { get; set; }

        public Guid ClienteId { get; set; }
        [JsonIgnore]
        public ClienteDto Cliente { get; set; }
    }
}
