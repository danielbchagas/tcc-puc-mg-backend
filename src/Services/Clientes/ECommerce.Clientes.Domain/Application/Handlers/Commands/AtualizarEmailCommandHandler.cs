using AutoMapper;
using ECommerce.Clientes.Domain.Application.Commands;
using ECommerce.Clientes.Domain.Application.Notifications;
using ECommerce.Clientes.Domain.Interfaces.Repositories;
using ECommerce.Clientes.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Application.Handlers.Commands
{
    public class AtualizarEmailCommandHandler : IRequestHandler<AtualizarEmailCommand, ValidationResult>
    {
        public AtualizarEmailCommandHandler(IEmailRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
            _validacao = new AtualizarEmailCommandValidator();
            _mapper = NovoMapeamento();
        }

        private readonly IEmailRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly AtualizarEmailCommandValidator _validacao;

        public async Task<ValidationResult> Handle(AtualizarEmailCommand request, CancellationToken cancellationToken)
        {
            var valido = _validacao.Validate(request);

            if (valido.IsValid)
            {
                var email = _mapper.Map<Email>(request);

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
                cfg.CreateMap<AtualizarEmailCommand, Email>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(c => c.Id))
                    .ForMember(dest => dest.Endereco, opt => opt.MapFrom(c => c.Endereco))
                    .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(c => c.ClienteId));
            });

            return configuration.CreateMapper();
        }
    }
}
