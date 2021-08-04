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
    public class CadastrarClienteCommandHandler : IRequestHandler<CadastrarClienteCommand, ValidationResult>
    {
        public CadastrarClienteCommandHandler(IClienteRepository repository, IMediator mediator)
        {
            _repository = repository;
            _validacoes = new RegistrarClienteCommandValidation();
            _mediator = mediator;
            _mapper = CriaMapeamento();
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
                var cliente = _mapper.Map<Dominio.Cliente>(request);

                await _repository.Adicionar(cliente);
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
                cfg.CreateMap<CadastrarClienteCommand, Dominio.Cliente>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(c => c.Id))
                    .ForMember(dest => dest.Nome, opt => opt.MapFrom(c => c.Nome))
                    .ForMember(dest => dest.Sobrenome, opt => opt.MapFrom(c => c.Sobrenome))
                    .ForMember(dest => dest.DataNascimento, opt => opt.MapFrom(c => c.Nascimento))
                    .ForMember(dest => dest.Documento, opt => opt.MapFrom(c => c.Documento))
                    .ForMember(dest => dest.Ativo, opt => opt.MapFrom(c => c.Ativo))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(c => c.Email))
                    .ForMember(dest => dest.Endereco, opt => opt.MapFrom(c => c.Endereco));
            });

            return configuration.CreateMapper();
        }
    }
}
