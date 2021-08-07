using System;
using FluentValidation;

namespace ECommerce.Catalogo.Domain.Models
{
    public class LogEvento : Entity
    {
        public LogEvento(Guid entidadeId, Guid usuarioId)
        {
            EntidadeId = entidadeId;
            UsuarioId = usuarioId;
        }

        public DateTime Momento { get; private set; }
        public Guid EntidadeId { get; private set; }
        public Guid UsuarioId { get; set; }
    }

    public class LogEventoValidator : AbstractValidator<LogEvento>
    {
        public LogEventoValidator()
        {
            RuleFor(le => le.Id)
                .NotEqual(Guid.Empty)
                .WithMessage(Enums.ErrosValidacao.NuloOuVazio.ToString());
            RuleFor(le => le.EntidadeId)
                .NotEqual(Guid.Empty)
                .WithMessage(Enums.ErrosValidacao.NuloOuVazio.ToString());
            RuleFor(le => le.UsuarioId)
                .NotEqual(Guid.Empty)
                .WithMessage(Enums.ErrosValidacao.NuloOuVazio.ToString());
        }
    }
}
