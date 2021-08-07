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
    public class AtualizarClienteCommandHandler : IRequestHandler<AtualizarClienteCommand, ValidationResult>
    {
        public AtualizarClienteCommandHandler(IClienteRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;

            _validador = new ClienteValidator();
            _mapper = NovoMapeamento();
        }

        private readonly IClienteRepository _repository;
        private readonly ClienteValidator _validador;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public async Task<ValidationResult> Handle(AtualizarClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = _mapper.Map<Models.Cliente>(request);

            var valido = _validador.Validate(cliente);

            if (valido.IsValid)
            {
                await _repository.Atualizar(cliente);
                var sucesso = await _repository.UnitOfWork.Commit();

                if (sucesso)
                    await _mediator.Publish(new ClienteCommitNotification(clienteId: cliente.Id, usuarioId: Guid.NewGuid()));
            }

            return await Task.FromResult(valido);
        }

        private IMapper NovoMapeamento()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AtualizarClienteCommand, Models.Cliente>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(c => c.Id))
                    .ForMember(dest => dest.Nome, opt => opt.MapFrom(c => c.Nome))
                    .ForMember(dest => dest.Sobrenome, opt => opt.MapFrom(c => c.Sobrenome))
                    .ForMember(dest => dest.DataNascimento, opt => opt.MapFrom(c => c.DataNascimento))
                    .ForMember(dest => dest.Ativo, opt => opt.MapFrom(c => c.Ativo));
            });

            return configuration.CreateMapper();
        }
    }
}
