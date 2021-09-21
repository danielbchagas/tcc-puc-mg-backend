using ECommerce.Carrinho.Application.Commands;
using ECommerce.Carrinho.Application.Notifications;
using ECommerce.Carrinho.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CarrinhoCliente = ECommerce.Carrinho.Domain.Models.Carrinho;

namespace ECommerce.Carrinho.Application.Handlers.Commands
{
    public class AdicionarCarrinhoCommandHandler : IRequestHandler<AdicionarCarrinhoCommand, ValidationResult>
    {
        private readonly ICarrinhoRepository _carrinhoRepository;
        private readonly IMediator _mediator;

        public AdicionarCarrinhoCommandHandler(ICarrinhoRepository carrinhoRepository, IMediator mediator)
        {
            _carrinhoRepository = carrinhoRepository;
            _mediator = mediator;
        }

        public async Task<ValidationResult> Handle(AdicionarCarrinhoCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResult();

            var carrinho = new CarrinhoCliente(request.ClienteId);

            validationResult = carrinho.Validar();

            if (!validationResult.IsValid)
                return await Task.FromResult(validationResult);

            await _carrinhoRepository.Adicionar(carrinho);
            var success = await _carrinhoRepository.UnitOfWork.Commit();

            if(success)
                await _mediator.Publish(new CarrinhoCommitNotification(carrinhoId: carrinho.Id, clienteId: request.ClienteId));

            return await Task.FromResult(validationResult);
        }
    }
}
