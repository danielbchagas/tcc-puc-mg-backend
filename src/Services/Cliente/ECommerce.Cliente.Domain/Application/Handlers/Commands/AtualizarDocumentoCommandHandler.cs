using AutoMapper;
using ECommerce.Cliente.Domain.Application.Commands;
using ECommerce.Cliente.Domain.Application.Notifications;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Domain.Application.Handlers.Commands
{
    public class AtualizarDocumentoCommandHandler : IRequestHandler<AtualizarDocumentoCommand, ValidationResult>
    {
        public AtualizarDocumentoCommandHandler(IDocumentoRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
            _validador = new DocumentoValidator();
            _mapper = NovoMapeamento();
        }

        private readonly IDocumentoRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly DocumentoValidator _validador;

        public async Task<ValidationResult> Handle(AtualizarDocumentoCommand request, CancellationToken cancellationToken)
        {
            var documento = await _repository.Buscar(request.Id);
            documento = _mapper.Map<Documento>(request);

            var valido = _validador.Validate(documento);

            if (valido.IsValid)
            {
                await _repository.Adicionar(documento);
                var sucesso = await _repository.UnitOfWork.Commit();

                if (sucesso)
                    await _mediator.Publish(new DocumentoCommitNotification(documentoId: documento.Id, usuarioId: request.ClienteId));
            }

            return await Task.FromResult(valido);
        }

        private IMapper NovoMapeamento()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AtualizarDocumentoCommand, Documento>()
                    .ForMember(dest => dest.Numero, opt => opt.MapFrom(c => c.Numero))
                    .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(c => c.ClienteId));
            });

            return configuration.CreateMapper();
        }
    }
}
