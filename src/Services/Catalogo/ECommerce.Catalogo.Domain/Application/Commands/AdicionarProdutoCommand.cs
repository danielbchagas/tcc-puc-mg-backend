using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Catalogo.Domain.Application.Commands
{
    public class AdicionarProdutoCommand : IRequest<ValidationResult>
    {
        public AdicionarProdutoCommand()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public long QuantidadeEstoque { get; set; }
        public decimal Preco { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
