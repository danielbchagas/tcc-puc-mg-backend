using AutoMapper;
using ECommerce.Clientes.Domain.Application.Commands;
using ECommerce.Clientes.Domain.Application.Notifications;
using ECommerce.Clientes.Domain.Interfaces.Repositories;
using ECommerce.Clientes.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Application.Handlers.Commands
{
    public class RegistrarClienteCommandHandler : IRequestHandler<RegistrarClienteCommand, ValidationResult>
    {
        public RegistrarClienteCommandHandler(IClienteRepository repository, IMediator mediator)
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

        public async Task<ValidationResult> Handle(RegistrarClienteCommand request, CancellationToken cancellationToken)
        {
            var valido = _validacoes.Validate(request);

            if (valido.IsValid)
            {
                var cliente = _mapper.Map<Cliente>(request);

                await _repository.Adicionar(cliente);
                var sucesso = await _repository.UnitOfWork.Commit();

                if (sucesso)
                    await _mediator.Publish(new ClienteCommitNotification(request.OrigemRequisicao, request.Uri, cliente.Id));
            }

            return await Task.FromResult(valido);
        }

        private IMapper CriaMapeamento()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegistrarClienteCommand, Cliente>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(c => c.ClienteId))
                    .ForMember(dest => dest.NomeFantasia, opt => opt.MapFrom(c => c.NomeFantasia))
                    .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(c => c.Cnpj))
                    .ForMember(dest => dest.Ativo, opt => opt.MapFrom(c => c.ClienteAtivo))
                    .ForMember(dest => dest.EnderecoId, opt => opt.MapFrom(_ => _.EnderecoId))
                    .ForMember(dest => dest.Endereco, opt => opt.MapFrom(e => new Endereco(e.EnderecoId, e.Logradouro, e.Bairro, e.Cidade, e.Cep, e.Estados, e.EnderecoAtivo)));
            });

            return configuration.CreateMapper();
        }
    }
}
