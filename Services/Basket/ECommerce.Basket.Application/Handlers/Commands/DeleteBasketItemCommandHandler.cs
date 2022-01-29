﻿using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Basket.Application.Handlers.Commands
{
    public class DeleteBasketItemCommandHandler : IRequestHandler<DeleteBasketItemCommand, ValidationResult>
    {
        private readonly ICustomerBasketRepository _basketRepository;

        public DeleteBasketItemCommandHandler(ICustomerBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<ValidationResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var basket = await _basketRepository.Get(request.CustomerBasketId);
            var item = basket.Items.FirstOrDefault(i => i.Id == request.Id);

            if (item == null)
            {
                validation.Errors.Add(new ValidationFailure("", "Item não encontrado."));
                return validation;
            }

            basket.RemoveItem(item);

            await _basketRepository.Update(basket);
            await _basketRepository.UnitOfWork.Commit();

            return await Task.FromResult(validation);
        }
    }
}
