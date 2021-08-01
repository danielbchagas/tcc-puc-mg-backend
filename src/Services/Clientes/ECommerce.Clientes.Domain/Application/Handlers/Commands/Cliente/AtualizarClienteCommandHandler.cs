using AutoMapper;
using ECommerce.Clientes.Domain.Application.Commands.Cliente;
using ECommerce.Clientes.Domain.Application.Notifications;
using ECommerce.Clientes.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Dominio = ECommerce.Clientes.Domain.Models;

namespace ECommerce.Clientes.Domain.Application.Handlers.Commands.Cliente
{
    public class AtualizarClienteCommandHandler : IRequestHandler<AtualizarClienteCommand, ValidationResult>
    {
        public AtualizarClienteCommandHandler(IClienteRepository repository, IMediator mediator)
        {
            _repository = repository;
            _validacoes = new AtualizarClienteCommandValidation();
            _mediator = mediator;

            _mapper = CriaMapeamento();
        }

        private readonly IClienteRepository _repository;
        private readonly AtualizarClienteCommandValidation _validacoes;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public async Task<ValidationResult> Handle(AtualizarClienteCommand request, CancellationToken cancellationToken)
        {
            var valido = _validacoes.Validate(request);

            if (valido.IsValid)
            {
                var cliente = _mapper.Map<Dominio.Cliente>(request);

                await _repository.Atualizar(cliente);
                var sucesso = await _repository.UnitOfWork.Commit();

                if (sucesso)
                    await _mediator.Publish(new ClienteCommitNotification("", "", cliente.Id));
            }

            return await Task.FromResult(valido);
        }

        private IMapper CriaMapeamento()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AtualizarClienteCommand, Dominio.Cliente>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(c => c.Id))
                    .ForMember(dest => dest.Nome, opt => opt.MapFrom(c => c.NomeFantasia))
                    .ForMember(dest => dest.Documento, opt => opt.MapFrom(c => c.Cnpj))
                    .ForMember(dest => dest.Ativo, opt => opt.MapFrom(c => c.Ativo));
            });

            return configuration.CreateMapper();
        }
    }
}
