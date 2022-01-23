using ECommerce.Carrinho.Application.Commands;
using ECommerce.Carrinho.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CarrinhoCliente = ECommerce.Carrinho.Domain.Models.CarrinhoCompras;

namespace ECommerce.Carrinho.Application.Handlers.Commands
{
    public class CreateCarrinhoCommandHandler : IRequestHandler<CreateCarrinhoCommand, ValidationResult>
    {
        private readonly ICarrinhoRepository _carrinhoRepository;
        private readonly IMediator _mediator;

        public CreateCarrinhoCommandHandler(ICarrinhoRepository carrinhoRepository, IMediator mediator)
        {
            _carrinhoRepository = carrinhoRepository;
            _mediator = mediator;
        }

        public async Task<ValidationResult> Handle(CreateCarrinhoCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResult();

            var carrinho = new CarrinhoCliente(request.ClienteId);

            validationResult = carrinho.Validar();

            if (!validationResult.IsValid)
                return await Task.FromResult(validationResult);

            await _carrinhoRepository.Adicionar(carrinho);
            var success = await _carrinhoRepository.UnitOfWork.Commit();

            return await Task.FromResult(validationResult);
        }
    }
}
