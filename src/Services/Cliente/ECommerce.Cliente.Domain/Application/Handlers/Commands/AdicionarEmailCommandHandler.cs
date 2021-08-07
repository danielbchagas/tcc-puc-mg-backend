using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Cliente.Domain.Application.Commands;
using ECommerce.Cliente.Domain.Application.Notifications;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Cliente.Domain.Application.Handlers.Commands
{
    public class AdicionarEmailCommandHandler : IRequestHandler<AdicionarEmailCommand, ValidationResult>
    {
        public AdicionarEmailCommandHandler(IEmailRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
            _validador = new EmailValidator();
            _mapper = NovoMapeamento();
        }

        private readonly IEmailRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly EmailValidator _validador;

        public async Task<ValidationResult> Handle(AdicionarEmailCommand request, CancellationToken cancellationToken)
        {
            var email = _mapper.Map<Email>(request);

            var valido = _validador.Validate(email);

            if (valido.IsValid)
            {
                await _repository.Adicionar(email);
                var sucesso = await _repository.UnitOfWork.Commit();

                if (sucesso)
                    await _mediator.Publish(new EmailCommitNotification(emailId: email.Id, usuarioId: Guid.NewGuid()));
            }

            return await Task.FromResult(valido);
        }

        private IMapper NovoMapeamento()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AdicionarEmailCommand, Email>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(c => c.Id))
                    .ForMember(dest => dest.Endereco, opt => opt.MapFrom(c => c.Endereco))
                    .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(c => c.ClienteId));
            });

            return configuration.CreateMapper();
        }
    }
}
