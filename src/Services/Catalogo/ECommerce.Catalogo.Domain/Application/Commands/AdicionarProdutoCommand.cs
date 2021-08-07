using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Catalogo.Domain.Application.Commands
{
    public class AdicionarProdutoCommand : IRequest<ValidationResult>
    {
        public AdicionarProdutoCommand()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Marca { get; set; }
        public string Nome { get; set; }
        public string Lote { get; set; }
        public string Imagem { get; set; }
        public string Observacao { get; set; }
        public long Quantidade { get; set; } // Poderia ser ulong
        public decimal Preco { get; set; }
        public bool Ativo { get; set; }
    }
}
