using ECommerce.Carrinho.Application.Commands;
using ECommerce.Carrinho.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Carrinho.Application.Handlers.Commands
{
    public class DeleteItemCarrinhoCommandHandler : IRequestHandler<DeleteItemCarrinhoCommand, ValidationResult>
    {
        private readonly IItemCarrinhoRepository _itemCarrinhoRepository;
        private readonly IMediator _mediator;

        public DeleteItemCarrinhoCommandHandler(IItemCarrinhoRepository itemCarrinhoRepository, IMediator mediator)
        {
            _itemCarrinhoRepository = itemCarrinhoRepository;
            _mediator = mediator;
        }

        public async Task<ValidationResult> Handle(DeleteItemCarrinhoCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResult();

            await _itemCarrinhoRepository.Excluir(request.Id);
            var success = await _itemCarrinhoRepository.UnitOfWork.Commit();

            return await Task.FromResult(validationResult);
        }
    }
}
