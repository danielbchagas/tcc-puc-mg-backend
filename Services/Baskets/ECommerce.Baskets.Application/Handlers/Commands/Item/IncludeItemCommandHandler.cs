using AutoMapper;
using ECommerce.Baskets.Application.Commands.Item;
using ECommerce.Baskets.Application.Constants;
using ECommerce.Baskets.Domain.Interfaces.Data;
using ECommerce.Baskets.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Baskets.Application.Handlers.Commands.Item
{
    public class IncludeItemCommandHandler : IRequestHandler<IncludeItemCommand, (ValidationResult, Domain.Models.Basket)>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public IncludeItemCommandHandler(IBasketRepository basketRepository, IItemRepository itemRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _basketRepository = basketRepository;
            _itemRepository = itemRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<(ValidationResult, Domain.Models.Basket)> Handle(IncludeItemCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var basket = await _basketRepository.Get(request.BasketId);

            if (basket is null)
            {
                validation.Errors.Add(new ValidationFailure("", BasketMessages.BasketNotFound));
                return (validation, basket);
            }

            foreach (var item in _mapper.Map<IEnumerable<Domain.Models.Item>>(request.Items))
                basket.AddOrUpdateItem(item);

            validation = basket.Validate();

            await _itemRepository.Update(basket.Items);
            await _basketRepository.Update(basket);
            await _unitOfWork.Commit();

            return (await Task.FromResult(validation), basket);
        }
    }
}
