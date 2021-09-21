using ECommerce.Carrinho.Application.Commands;
using ECommerce.Carrinho.Application.Notifications;
using ECommerce.Carrinho.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Carrinho.Application.Handlers.Commands
{
    public class ExcluirCarrinhoCommandHandler : IRequestHandler<ExcluirCarrinhoCommand, ValidationResult>
    {
        private readonly ICarrinhoRepository _carrinhoRepository;
        private readonly IMediator _mediator;

        public ExcluirCarrinhoCommandHandler(ICarrinhoRepository carrinhoRepository, IMediator mediator)
        {
            _carrinhoRepository = carrinhoRepository;
            _mediator = mediator;
        }

        public async Task<ValidationResult> Handle(ExcluirCarrinhoCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResult();

            await _carrinhoRepository.Excluir(request.Id);
            var success = await _carrinhoRepository.UnitOfWork.Commit();

            if (success)
                await _mediator.Publish(new CarrinhoCommitNotification(carrinhoId: request.Id, clienteId: request.ClienteId));

            return await Task.FromResult(validationResult);
        }
    }
}
