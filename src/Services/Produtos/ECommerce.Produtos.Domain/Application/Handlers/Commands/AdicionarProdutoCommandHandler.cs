﻿using AutoMapper;
using ECommerce.Produtos.Domain.Application.Commands;
using ECommerce.Produtos.Domain.Application.Notifications;
using ECommerce.Produtos.Domain.Interfaces.Repositories;
using ECommerce.Produtos.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Produtos.Domain.Application.Handlers.Commands
{
    public class AdicionarProdutoCommandHandler : IRequestHandler<AdicionarProdutoCommand, ValidationResult>
    {
        public AdicionarProdutoCommandHandler(IProdutoRepository repository, IMediator mediator)
        {
            _repository = repository;
            _validacao = new ProdutoValidator();
            _mediator = mediator;
            _mapper = NovoMapper();
        }

        private readonly IProdutoRepository _repository;
        private readonly ProdutoValidator _validacao;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public async Task<ValidationResult> Handle(AdicionarProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = _mapper.Map<Produto>(request);

            var valido = _validacao.Validate(produto);

            if (valido.IsValid)
            {
                await _repository.Adicionar(produto);
                var sucesso = await _repository.UnitOfWork.Commit();

                if (sucesso)
                    await _mediator.Publish(new ProdutoCommitNotification(produtoId: request.Id, usuarioId: Guid.NewGuid())); // Trocar pelo ID do usuário da aplicação
            }

            return await Task.FromResult(valido);
        }

        private IMapper NovoMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AdicionarProdutoCommand, Produto>();
            });

            return configuration.CreateMapper();
        }
    }
}
