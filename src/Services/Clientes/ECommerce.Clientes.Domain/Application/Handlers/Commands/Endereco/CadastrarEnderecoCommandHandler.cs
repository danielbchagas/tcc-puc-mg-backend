﻿using AutoMapper;
using ECommerce.Clientes.Domain.Application.Commands.Endereco;
using ECommerce.Clientes.Domain.Application.Notifications;
using ECommerce.Clientes.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Dominio = ECommerce.Clientes.Domain.Models;

namespace ECommerce.Clientes.Domain.Application.Handlers.Commands.Endereco
{
    public class CadastrarEnderecoCommandHandler : IRequestHandler<CadastrarEnderecoCommand, ValidationResult>
    {
        public CadastrarEnderecoCommandHandler(IEnderecoRepository repository, IClienteRepository clienteRepository, IMediator mediator)
        {
            _repository = repository;
            _clienteRepository = clienteRepository;
            _validacoes = new RegistrarEnderecoCommandValidation();
            _mediator = mediator;
            _mapper = CriaMapeamento();
        }

        private readonly IEnderecoRepository _repository;
        private readonly IClienteRepository _clienteRepository;
        private readonly RegistrarEnderecoCommandValidation _validacoes;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public async Task<ValidationResult> Handle(CadastrarEnderecoCommand request, CancellationToken cancellationToken)
        {
            var valido = _validacoes.Validate(request);

            if (valido.IsValid)
            {
                var endereco = _mapper.Map<Dominio.Endereco>(request);
                
                // Vincula o cliente ao endereço
                endereco.ClienteId = request.ClienteId;
                endereco.Cliente = await _clienteRepository.Buscar(request.ClienteId);

                await _repository.Adicionar(endereco);
                var sucesso = await _repository.UnitOfWork.Commit();

                if (sucesso)
                    await _mediator.Publish(new EnderecoCommitNotification("", "", endereco.Id));
            }

            return await Task.FromResult(valido);
        }

        private IMapper CriaMapeamento()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CadastrarEnderecoCommand, Dominio.Endereco>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(c => c.Id))
                    .ForMember(dest => dest.Logradouro, opt => opt.MapFrom(c => c.Logradouro))
                    .ForMember(dest => dest.Bairro, opt => opt.MapFrom(c => c.Bairro))
                    .ForMember(dest => dest.Cidade, opt => opt.MapFrom(c => c.Cidade))
                    .ForMember(dest => dest.Estado, opt => opt.MapFrom(_ => _.Estado))
                    .ForMember(dest => dest.Cep, opt => opt.MapFrom(_ => _.Cep))
                    .ForMember(dest => dest.Ativo, opt => opt.MapFrom(_ => _.Ativo))
                    .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(_ => _.ClienteId));
            });

            return configuration.CreateMapper();
        }
    }
}
