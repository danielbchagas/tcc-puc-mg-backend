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
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public async Task<ValidationResult> Handle(AtualizarClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _repository.Buscar(request.Id);
            cliente = _mapper.Map<Models.Cliente>(request);

            var valido = _validador.Validate(cliente);

            if (valido.IsValid)
            {
                await _repository.Atualizar(cliente);
                var sucesso = await _repository.UnitOfWork.Commit();

                if (sucesso)
                    await _mediator.Publish(new ClienteCommitNotification(clienteId: cliente.Id, usuarioId: request.Id));
            }

            return await Task.FromResult(valido);
        }

        private IMapper NovoMapeamento()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AtualizarClienteCommand, Models.Cliente>()
                    .ForMember(dest => dest.Nome, opt => opt.MapFrom(c => c.Nome))
                    .ForMember(dest => dest.Sobrenome, opt => opt.MapFrom(c => c.Sobrenome));
            });

            return configuration.CreateMapper();
        }
    }
}
