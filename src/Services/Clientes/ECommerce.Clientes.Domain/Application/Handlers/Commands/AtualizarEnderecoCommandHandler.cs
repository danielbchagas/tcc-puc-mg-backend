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
    public class AtualizarEnderecoCommandHandler : IRequestHandler<AtualizarEnderecoCommand, ValidationResult>
    {
        public AtualizarEnderecoCommandHandler(IEnderecoRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
            _validacoes = new AtualizarEnderecoCommandValidation();
            _mapper = NovoMapeamento();
        }

        private readonly IEnderecoRepository _repository;
        private readonly AtualizarEnderecoCommandValidation _validacoes;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public async Task<ValidationResult> Handle(AtualizarEnderecoCommand request, CancellationToken cancellationToken)
        {
            var valido = _validacoes.Validate(request);

            if (valido.IsValid)
            {
                var endereco = _mapper.Map<Endereco>(request);

                await _repository.Atualizar(endereco);
                var sucesso = await _repository.UnitOfWork.Commit();

                if (sucesso)
                    await _mediator.Publish(new EnderecoCommitNotification(enderecoId: endereco.Id, usuarioId: Guid.NewGuid()));
            }

            return await Task.FromResult(valido);
        }

        private IMapper NovoMapeamento()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CadastrarEnderecoCommand, Endereco>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(c => c.Id))
                    .ForMember(dest => dest.Logradouro, opt => opt.MapFrom(c => c.Logradouro))
                    .ForMember(dest => dest.Bairro, opt => opt.MapFrom(c => c.Bairro))
                    .ForMember(dest => dest.Cidade, opt => opt.MapFrom(c => c.Cidade))
                    .ForMember(dest => dest.Estado, opt => opt.MapFrom(_ => _.Estado))
                    .ForMember(dest => dest.Cep, opt => opt.MapFrom(_ => _.Cep))
                    .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(_ => _.ClienteId));
            });

            return configuration.CreateMapper();
        }
    }
}
