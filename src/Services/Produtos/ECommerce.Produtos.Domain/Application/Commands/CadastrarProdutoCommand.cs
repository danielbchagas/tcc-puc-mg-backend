using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Produtos.Domain.Application.Commands
{
    public class CadastrarProdutoCommand : IRequest<ValidationResult>
    {
        public CadastrarProdutoCommand()
        {
            Id = Guid.NewGuid();
        }

        // Produto
        public Guid Id { get; set; }
        public string Marca { get; set; }
        public string Nome { get; set; }
        public string Lote { get; set; }
        public DateTime Fabricacao { get; set; }
        public DateTime Vencimento { get; set; }
        public string Imagem { get; set; }
        public string Observacao { get; set; }
        public long Quantidade { get; set; } // Poderia ser ulong
        public bool Ativo { get; set; }
    }
}
