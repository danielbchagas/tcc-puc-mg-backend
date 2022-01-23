﻿using ECommerce.Carrinho.Application.Commands;
using ECommerce.Carrinho.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Carrinho.Application.Handlers.Commands
{
    public class DeleteCarrinhoCommandHandler : IRequestHandler<DeleteCarrinhoCommand, ValidationResult>
    {
        private readonly ICarrinhoRepository _carrinhoRepository;
        private readonly IMediator _mediator;

        public DeleteCarrinhoCommandHandler(ICarrinhoRepository carrinhoRepository, IMediator mediator)
        {
            _carrinhoRepository = carrinhoRepository;
            _mediator = mediator;
        }

        public async Task<ValidationResult> Handle(DeleteCarrinhoCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResult();

            await _carrinhoRepository.Excluir(request.Id);
            var success = await _carrinhoRepository.UnitOfWork.Commit();

            return await Task.FromResult(validationResult);
        }
    }
}