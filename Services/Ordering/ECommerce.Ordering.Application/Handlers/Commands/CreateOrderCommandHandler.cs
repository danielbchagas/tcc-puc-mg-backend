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
        private readonly IOrderRepository _repository;

        public CreateOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<ValidationResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order(
                id: request.Id, 
                fullName: request.FullName, 
                document: request.Document, 
                phone: request.Phone, 
                email: request.Email, 
                firstLine: request.FirstLine, 
                secondLine: request.SecondLine, 
                city: request.City, 
                state: request.State, 
                zipCode: request.ZipCode
            );

            order.Items = request.Items;

            order.Totalize();

            var validation = order.Validate();

            if (validation.IsValid)
            {
                await _repository.Create(order);
                await _repository.UnitOfWork.Commit();
            }

            return await Task.FromResult(validation);
        }
    }
}
