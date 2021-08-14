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
    public class AtualizarEnderecoCommandHandler : IRequestHandler<AtualizarEnderecoCommand, ValidationResult>
    {
        public AtualizarEnderecoCommandHandler(IEnderecoRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
            _validador = new EnderecoValidator();
            _mapper = NovoMapeamento();
        }

        private readonly IEnderecoRepository _repository;
        private readonly EnderecoValidator _validador;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public async Task<ValidationResult> Handle(AtualizarEnderecoCommand request, CancellationToken cancellationToken)
        {
            var endereco = await _repository.Buscar(request.Id);
            endereco = _mapper.Map<Endereco>(request);

            var valido = _validador.Validate(endereco);

            if (valido.IsValid)
            {
                await _repository.Atualizar(endereco);
                var sucesso = await _repository.UnitOfWork.Commit();

                if (sucesso)
                    await _mediator.Publish(new EnderecoCommitNotification(enderecoId: endereco.Id, usuarioId: request.ClienteId));
            }

            return await Task.FromResult(valido);
        }

        private IMapper NovoMapeamento()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AtualizarEnderecoCommand, Endereco>()
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
