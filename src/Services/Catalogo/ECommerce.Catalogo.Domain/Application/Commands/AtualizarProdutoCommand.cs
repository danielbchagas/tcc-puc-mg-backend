using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Catalogo.Domain.Application.Commands
{
    public class AtualizarProdutoCommand : IRequest<ValidationResult>
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public long QuantidadeEstoque { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
