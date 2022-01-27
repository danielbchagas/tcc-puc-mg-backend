using System.Threading;
using System.Threading.Tasks;
using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Basket.Application.Handlers.Commands
{
    public class DeleteBasketItemCommandHandler : IRequestHandler<DeleteBasketItemCommand, ValidationResult>
    {
        private readonly IBasketItemRepository _itemRepository;
        
        public DeleteBasketItemCommandHandler(IBasketItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<ValidationResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            await _itemRepository.Delete(request.Id);
            await _itemRepository.UnitOfWork.Commit();

            return await Task.FromResult(validation);
        }
    }
}
