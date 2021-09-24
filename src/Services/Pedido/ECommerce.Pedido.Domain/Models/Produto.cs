using FluentValidation;
using FluentValidation.Results;
using System;

namespace ECommerce.Pedido.Domain.Models
{
    public class Produto : Entity
    {
        public Produto(string descricao, string nome, string imagem, long quantidade, decimal valor, bool ativo = true)
        {
            Descricao = descricao;
            Nome = nome;
            Imagem = imagem;
            Quantidade = quantidade;
            Valor = valor;
            Ativo = ativo;
            DataCadastro = DateTime.Now;
        }

        #region Propriedades
        public string Descricao { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public long Quantidade { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        #endregion

        #region Métodos
        public ValidationResult Validar()
        {
            return new ProdutoValidator().Validate(this);
        }
        #endregion
    }

    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {
            RuleFor(p => p.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} não pode ser nulo ou vazio");
            RuleFor(p => p.Descricao)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio");
            RuleFor(p => p.Nome)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio");
            RuleFor(p => p.Imagem)
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio");
            RuleFor(p => p.Quantidade)
                .GreaterThan(0)
                .WithMessage("{PropertyName} tem um valor menor do que o esperado!")
                .LessThan(long.MaxValue)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!");
            RuleFor(p => p.Valor)
                .GreaterThan(0)
                .WithMessage("{PropertyName} tem um valor menor do que o esperado!")
                .LessThan(decimal.MaxValue)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!");
        }
    }
}
