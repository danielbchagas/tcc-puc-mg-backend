using AutoMapper;
using ECommerce.Cliente.Domain.Application.Commands;
using ECommerce.Cliente.Domain.Application.Notifications;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Domain.Application.Handlers.Commands
{
    public class AtualizarTelefoneCommandHandler : IRequestHandler<AtualizarTelefoneCommand, ValidationResult>
    {
        public AtualizarTelefoneCommandHandler(ITelefoneRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
            _validacoes = new TelefoneValidator();
            _mapper = NovoMapeamento();
        }

        private readonly ITelefoneRepository _repository;
        private readonly IMediator _mediator;
        private readonly TelefoneValidator _validacoes;
        private readonly IMapper _mapper;

        public async Task<ValidationResult> Handle(AtualizarTelefoneCommand request, CancellationToken cancellationToken)
        {
            var telefone = _mapper.Map<Telefone>(request);

            var valido = _validacoes.Validate(telefone);

            if (valido.IsValid)
            {
                await _repository.Atualizar(telefone);
                var sucesso = await _repository.UnitOfWork.Commit();

                if (sucesso)
                    await _mediator.Publish(new TelefoneCommitNotification(telefoneId: telefone.Id, usuarioId: Guid.NewGuid()));
            }

            return await Task.FromResult(valido);
        }

        private IMapper NovoMapeamento()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AtualizarTelefoneCommand, Telefone>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(t => t.Id))
                    .ForMember(dest => dest.Numero, opt => opt.MapFrom(t => t.ClienteId))
                    .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(t => t.ClienteId));
            });

            return configuration.CreateMapper();
        }
    }
}
