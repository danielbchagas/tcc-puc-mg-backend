using AutoMapper;
using ECommerce.Baskets.Application.Commands.Item;
using ECommerce.Baskets.Application.Constants;
using ECommerce.Baskets.Domain.Interfaces.Data;
using ECommerce.Baskets.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Baskets.Application.Handlers.Commands.Item
{
    public class RemoveItemCommandHandler : IRequestHandler<RemoveItemCommand, (ValidationResult, Domain.Models.Basket)>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveItemCommandHandler(IBasketRepository basketRepository, IItemRepository itemRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _basketRepository = basketRepository;
            _itemRepository = itemRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<(ValidationResult, Domain.Models.Basket)> Handle(RemoveItemCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var basket = await _basketRepository.Get(request.BasketId);

            if (basket is null)
            {
                validation.Errors.Add(new ValidationFailure("", BasketMessages.BasketNotFound));
                return (validation, basket);
            }

            basket.RemoveItems(_mapper.Map<Domain.Models.Item>(basket.Items.FirstOrDefault(f => f.Id == request.ItemId)));

            validation = basket.Validate();

            await _itemRepository.Update(basket.Items);
            await _basketRepository.Update(basket);
            await _unitOfWork.Commit();

            return (await Task.FromResult(validation), basket);
        }
    }
}
