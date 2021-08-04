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
    public class CadastrarClienteCommandHandler : IRequestHandler<CadastrarClienteCommand, ValidationResult>
    {
        public CadastrarClienteCommandHandler(IClienteRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
            _validacoes = new RegistrarClienteCommandValidation();
            _mapper = NovoMapeamento();
        }

        private readonly IClienteRepository _repository;
        private readonly RegistrarClienteCommandValidation _validacoes;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public async Task<ValidationResult> Handle(CadastrarClienteCommand request, CancellationToken cancellationToken)
        {
            var valido = _validacoes.Validate(request);

            if (valido.IsValid)
            {
                var cliente = _mapper.Map<Cliente>(request);

                await _repository.Adicionar(cliente);
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
                cfg.CreateMap<CadastrarClienteCommand, Cliente>()
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
