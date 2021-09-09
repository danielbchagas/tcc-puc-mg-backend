using System;

namespace ECommerce.Compras.Gateway.Models.Cliente
{
    public class AtualizarClienteDto
    {
        public AtualizarClienteDto(Guid id, string nome, string sobrenome, bool ativo)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Ativo = ativo;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public bool Ativo { get; set; }
    }
}
