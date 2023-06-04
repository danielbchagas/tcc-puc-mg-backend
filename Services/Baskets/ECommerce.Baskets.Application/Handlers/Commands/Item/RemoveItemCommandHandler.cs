using AutoMapper;
using ECommerce.Baskets.Application.Commands.Item;
using ECommerce.Baskets.Application.Constants;
using ECommerce.Baskets.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Baskets.Application.Handlers.Commands.Item
{
    public class RemoveItemCommandHandler : IRequestHandler<RemoveItemCommand, (ValidationResult, Domain.Models.Basket)>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public RemoveItemCommandHandler(IBasketRepository basketRepository, IItemRepository itemRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _itemRepository = itemRepository;
            _mapper = mapper;
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

            basket.RemoveItems(_mapper.Map<Domain.Models.Item>(request.Item));

            validation = basket.Validate();

            await Transaction(basket);

            return (await Task.FromResult(validation), basket);
        }

        private async Task Transaction(Domain.Models.Basket basket)
        {
            IDbContextTransaction transaction = null;

            try
            {
                transaction = await _basketRepository.UnitOfWork.OpenTransaction();

                await _itemRepository.Update(basket.Items);
                await _basketRepository.Update(basket);

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
