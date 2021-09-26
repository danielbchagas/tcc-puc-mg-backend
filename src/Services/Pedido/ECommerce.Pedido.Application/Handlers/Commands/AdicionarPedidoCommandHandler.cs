using ECommerce.Pedido.Application.Commands;
using ECommerce.Pedido.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using PedidoCliente = ECommerce.Pedido.Domain.Models.Pedido;

namespace ECommerce.Pedido.Application.Handlers.Commands
{
    public class AdicionarPedidoCommandHandler : IRequestHandler<AdicionarPedidoCommand, ValidationResult>
    {
        private readonly IMediator _mediator;
        private readonly IPedidoRepository _repository;

        public AdicionarPedidoCommandHandler(IMediator mediator, IPedidoRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<ValidationResult> Handle(AdicionarPedidoCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResult();

            var pedido = new PedidoCliente(status: request.Status, cliente: request.Cliente, produtos: request.Produtos);

            validationResult = pedido.Validar();

            if (validationResult.IsValid)
            {
                await _repository.Adicionar(pedido);
                await _repository.UnitOfWork.Commit();
            }

            return await Task.FromResult(validationResult);
        }
    }
}
