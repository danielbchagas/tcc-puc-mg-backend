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
    public class AtualizarDocumentoCommandHandler : IRequestHandler<AtualizarDocumentoCommand, ValidationResult>
    {
        public AtualizarDocumentoCommandHandler(IDocumentoRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
            _validacao = new AtualizarDocumentoCommandValidator();
            _mapper = NovoMapeamento();
        }

        private readonly IDocumentoRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly AtualizarDocumentoCommandValidator _validacao;

        public async Task<ValidationResult> Handle(AtualizarDocumentoCommand request, CancellationToken cancellationToken)
        {
            var valido = _validacao.Validate(request);

            if (valido.IsValid)
            {
                var documento = _mapper.Map<Documento>(request);

                await _repository.Adicionar(documento);
                var sucesso = await _repository.UnitOfWork.Commit();

                if (sucesso)
                    await _mediator.Publish(new DocumentoCommitNotification(documentoId: documento.Id, usuarioId: Guid.NewGuid()));
            }

            return await Task.FromResult(valido);
        }

        private IMapper NovoMapeamento()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AtualizarDocumentoCommand, Documento>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(c => c.Id))
                    .ForMember(dest => dest.Numero, opt => opt.MapFrom(c => c.Numero))
                    .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(c => c.ClienteId));
            });

            return configuration.CreateMapper();
        }
    }
}
