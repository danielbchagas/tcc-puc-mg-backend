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
    public class AtualizarClienteCommandHandler : IRequestHandler<AtualizarClienteCommand, ValidationResult>
    {
        public AtualizarClienteCommandHandler(IClienteRepository repository, IMediator mediator)
        {
            _repository = repository;
            _validacoes = new AtualizarClienteCommandValidation();
            _mediator = mediator;

            #region AutoMapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AtualizarClienteCommand, Cliente>();
            });

            _mapper = configuration.CreateMapper();
            #endregion
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
                var cliente = _mapper.Map<Cliente>(request);

                await _repository.Atualizar(cliente);
                var sucesso = await _repository.UnitOfWork.Commit();

                if (sucesso)
                    await _mediator.Publish(new ClienteCommitNotification(request.OrigemRequisicao, request.Uri, cliente.Id));
            }

            return await Task.FromResult(valido);
        }
    }
}
