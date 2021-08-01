using FluentValidation;
using System;

namespace ECommerce.Produtos.Domain.Models
{
    public class LogEvento : Entity
    {
        public LogEvento(Guid produtoId, Guid usuarioId)
        {
            ProdutoId = produtoId;
            UsuarioId = usuarioId;
        }

        public DateTime Momento { get; private set; }
        public Guid ProdutoId { get; private set; }
        public Guid UsuarioId { get; set; }
    }

    public class LogEventoValidation : AbstractValidator<LogEvento>
    {
        public LogEventoValidation()
        {
            RuleFor(le => le.Id).NotEqual(Guid.Empty).WithMessage("{PropertyName} é inválido!");
            RuleFor(le => le.ProdutoId).NotEqual(Guid.Empty).WithMessage("{PropertyName} é inválido!");
            RuleFor(le => le.UsuarioId).NotEqual(Guid.Empty).WithMessage("{PropertyName} é inválido!");
        }
    }
}
