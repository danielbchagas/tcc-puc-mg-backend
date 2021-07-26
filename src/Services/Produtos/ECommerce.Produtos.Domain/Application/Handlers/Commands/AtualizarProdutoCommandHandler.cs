using AutoMapper;
using ECommerce.Produtos.Domain.Application.Commands;
using ECommerce.Produtos.Domain.Application.Notifications;
using ECommerce.Produtos.Domain.Interfaces.Repositories;
using ECommerce.Produtos.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Produtos.Domain.Application.Handlers.Commands
{
    public class AtualizarProdutoCommandHandler : IRequestHandler<AtualizarProdutoCommand, ValidationResult>
    {
        public AtualizarProdutoCommandHandler(IProdutoRepository repository, IMediator mediator)
        {
            _repository = repository;
            _validacoes = new AtualizarProdutoCommandValidation();
            _mediator = mediator;

            #region AutoMapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AtualizarProdutoCommand, Produto>();
            });

            _mapper = configuration.CreateMapper();
            #endregion
        }

        private readonly IProdutoRepository _repository;
        private readonly AtualizarProdutoCommandValidation _validacoes;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public async Task<ValidationResult> Handle(AtualizarProdutoCommand request, CancellationToken cancellationToken)
        {
            var valido = _validacoes.Validate(request);

            if (valido.IsValid)
            {
                var produto = _mapper.Map<Produto>(request);

                await _repository.Atualizar(produto);
                var sucesso = await _repository.UnitOfWork.Commit();

                if (sucesso)
                    await _mediator.Publish(new ProdutoCommitNotification(request.OrigemRequisicao, request.Uri, request.Id));
            }

            return await Task.FromResult(valido);
        }
    }
}
