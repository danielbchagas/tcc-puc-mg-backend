using System.Threading;
using System.Threading.Tasks;
using ECommerce.Carts.Application.Commands;
using ECommerce.Carts.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Carts.Application.Handlers.Commands
{
    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, ValidationResult>
    {
        private readonly IItemRepository _itemRepository;
        
        public DeleteItemCommandHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<ValidationResult> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            await _itemRepository.Delete(request.Id);
            await _itemRepository.UnitOfWork.Commit();

            return await Task.FromResult(validation);
        }
    }
}
