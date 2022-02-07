using System.Threading;
using System.Threading.Tasks;
using ECommerce.Ordering.Application.Commands;
using ECommerce.Ordering.Domain.Interfaces.Repositories;
using ECommerce.Ordering.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Ordering.Application.Handlers.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ValidationResult>
    {
        private readonly IMediator _mediator;
        private readonly IPedidoRepository _repository;

        public CreateOrderCommandHandler(IMediator mediator, IPedidoRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<ValidationResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order(fullName: request.FullName, document: request.Document, phone: request.Phone, email: request.Email, firstLine: request.FirstLine, secondLine: request.SecondLine, city: request.City, state: request.State, zipCode: request.ZipCode);

            var validation = order.Validate();

            if (validation.IsValid)
            {
                await _repository.Adicionar(order);
                await _repository.UnitOfWork.Commit();
            }

            return await Task.FromResult(validation);
        }
    }
}
