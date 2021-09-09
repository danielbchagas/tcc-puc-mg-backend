using System;
using System.ComponentModel;

namespace ECommerce.Compras.Gateway.Models.Cliente
{
    public class BuscarClienteDto
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
        public string Numero { get;  set; }
    }

    public class Email
    {
        public Guid Id { get; set; }
        public string Endereco { get; set; }
    }

    public class Endereco
    {
        public Guid Id { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public Estados Estado { get; set; }
    }

    public class Telefone
    {
        public Guid Id { get; set; }
        public string Numero { get; set; }
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
