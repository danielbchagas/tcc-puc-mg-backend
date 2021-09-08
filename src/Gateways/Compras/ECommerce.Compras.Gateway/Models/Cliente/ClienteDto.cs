using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ECommerce.Compras.Gateway.Models.Cliente
{
    public class ClienteDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public bool Ativo { get; set; }

        public Documento Documento { get; set; }
        public Email Email { get; set; }
        public Telefone Telefone { get; set; }
        public Endereco Endereco { get; set; }
    }

    public class Documento
    {
        public Guid Id { get; set; }
        public string Numero { get; private set; }

        public Guid ClienteId { get; private set; }
        [JsonIgnore]
        public ClienteDto Cliente { get; private set; }
    }

    public class Email
    {
        public Guid Id { get; set; }
        public string Endereco { get; private set; }

        public Guid ClienteId { get; private set; }
        [JsonIgnore]
        public ClienteDto Cliente { get; private set; }
    }

    public class Endereco
    {
        public Guid Id { get; set; }
        public string Logradouro { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Cep { get; private set; }
        public Estados Estado { get; private set; }

        // Relacionamento
        public Guid ClienteId { get; private set; }
        [JsonIgnore]
        public ClienteDto Cliente { get; private set; }
    }

    public class Telefone
    {
        public Guid Id { get; set; }
        public string Numero { get; private set; }

        public Guid ClienteId { get; private set; }
        [JsonIgnore]
        public ClienteDto Cliente { get; private set; }
    }

    public enum Estados
    {
        [Description("Acre")]
        AC,
        [Description("Alagoas")]
        AL,
        [Description("Amapá")]
        AP,
        [Description("Amazonas")]
        AM,
        [Description("Bahia")]
        BA,
        [Description("Ceará")]
        CE,
        [Description("Distrito Federal")]
        DF,
        [Description("Espirito Santo")]
        ES,
        [Description("Goiás")]
        GO,
        [Description("Maranhão")]
        MA,
        [Description("Mato Grosso")]
        MT,
        [Description("Mato Grosso do Sul")]
        MS,
        [Description("Minas Gerais")]
        MG,
        [Description("Pará")]
        PA,
        [Description("Paraiba")]
        PB,
        [Description("Paraná")]
        PR,
        [Description("Pernambuco")]
        PE,
        [Description("Piauí")]
        PI,
        [Description("Rio de Janeiro")]
        RJ,
        [Description("Rio Grande do Norte")]
        RN,
        [Description("Rio Grande do Sul")]
        RS,
        [Description("Rondônia")]
        RO,
        [Description("Roraima")]
        RR,
        [Description("Santa Catarina")]
        SC,
        [Description("São Paulo")]
        SP,
        [Description("Sergipe")]
        SE,
        [Description("Tocantis")]
        TO
    }
}
