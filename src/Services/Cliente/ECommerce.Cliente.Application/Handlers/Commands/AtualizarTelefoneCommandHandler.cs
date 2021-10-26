﻿using ECommerce.Cliente.Application.Commands;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Application.Handlers.Commands
{
    public class AtualizarTelefoneCommandHandler : IRequestHandler<AtualizarTelefoneCommand, ValidationResult>
    {
        public AtualizarTelefoneCommandHandler(ITelefoneRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        private readonly ITelefoneRepository _repository;
        private readonly IMediator _mediator;

        public async Task<ValidationResult> Handle(AtualizarTelefoneCommand request, CancellationToken cancellationToken)
        {
            var telefone = await _repository.Buscar(request.Id);
            telefone.Numero = request.Numero;
            
            var valido = telefone.Validar();

            if (valido.IsValid)
            {
                await _repository.Atualizar(telefone);
                var sucesso = await _repository.UnitOfWork.Commit();
            }

            return await Task.FromResult(valido);
        }
    }
}