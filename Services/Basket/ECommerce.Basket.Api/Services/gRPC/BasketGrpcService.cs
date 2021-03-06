using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Application.Queries;
using ECommerce.Basket.Api.Protos;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Basket.Api.Services.gRPC
{
    [Authorize]
    public class BasketGrpcService : ShoppingBasketService.ShoppingBasketServiceBase
    {
        private readonly IMediator _mediator;

        public BasketGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region ShoppingBasket
        public override async Task<CreateBasketResponse> CreateBasket(CreateBasketRequest request, ServerCallContext context)
        {
            var createCustomerBasketCommand = new CreateShoppingBasketCommand(
                id: Guid.Parse(request.Id),
                customerId: Guid.Parse(request.Customerid)
            );

            var result = await _mediator.Send(createCustomerBasketCommand);

            return new CreateBasketResponse()
            {
                Isvalid = result.IsValid,
                Message = JsonSerializer.Serialize(result.Errors)
            };
        }

        public override async Task<GetBasketByIdResponse> GetBasketById(GetBasketByIdRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetShoppingBasketByIdQuery(Guid.Parse(request.Id)));

            if (result == null)
            {
                return new GetBasketByIdResponse
                {
                    Basket = null
                };
            }

            var response = new GetBasketByIdResponse
            {
                Basket = new ShoppingBasket
                {
                    Id = Convert.ToString(result.Id),
                    Customerid = Convert.ToString(result.CustomerId),
                    Value = Convert.ToDouble(result.Value),
                    Registrationdate = new date
                    {
                        Day = result.RegistrationDate.Day,
                        Month = result.RegistrationDate.Month,
                        Year = result.RegistrationDate.Year,
                    },
                    Isended = result.IsEnded
                }
            };

            foreach (var item in result.Items)
            {
                response.Basket.Items.Add(new BasketItem
                {
                    Id = Convert.ToString(item.Id),
                    Name = item.Name,
                    Image = item.Image,
                    Quantity = item.Quantity,
                    Value = Convert.ToDouble(item.Value),
                    Productid = Convert.ToString(item.ProductId),
                    Shoppingbasketid = Convert.ToString(item.ShoppingBasketId)
                });
            }

            return response;
        }

        public override async Task<GetBasketByCustomerResponse> GetBasketByCustomer(GetBasketByCustomerRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetShoppingBasketByCustomerQuery(Guid.Parse(request.Customerid)));

            if(result == null)
            {
                return new GetBasketByCustomerResponse
                {
                    Basket = null
                };
            }

            var response = new GetBasketByCustomerResponse
            {
                Basket = new ShoppingBasket
                {
                    Id = Convert.ToString(result.Id),
                    Customerid = Convert.ToString(result.CustomerId),
                    Value = Convert.ToDouble(result.Value),
                    Registrationdate = new date
                    {
                        Day = result.RegistrationDate.Day,
                        Month = result.RegistrationDate.Month,
                        Year = result.RegistrationDate.Year,
                    },
                    Isended = result.IsEnded
                }
            };

            foreach(var item in result.Items)
            {
                response.Basket.Items.Add(new BasketItem
                {
                    Id = Convert.ToString(item.Id),
                    Name = item.Name,
                    Image = item.Image,
                    Quantity = item.Quantity,
                    Value = Convert.ToDouble(item.Value),
                    Productid = Convert.ToString(item.ProductId),
                    Shoppingbasketid = Convert.ToString(item.ShoppingBasketId)
                });
            }

            return response;
        }

        public override async Task<GetAllBasketResponse> GetAllBasket(GetAllBasketRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetShoppingBasketQuery(Guid.Parse(request.Customerid)));

            var response = new GetAllBasketResponse
            {

            };

            foreach (var basket in result)
            {
                var newBasket = new ShoppingBasket
                {
                    Id = Convert.ToString(basket.Id),
                    Customerid = Convert.ToString(basket.CustomerId),
                    Value = Convert.ToDouble(basket.Value),
                    Registrationdate = new date
                    {
                        Day = basket.RegistrationDate.Day,
                        Month = basket.RegistrationDate.Month,
                        Year = basket.RegistrationDate.Year,
                    },
                    Isended = basket.IsEnded
                };

                response.Baskets.Add(newBasket);
            }

            return response;
        }

        public override async Task<DeleteBasketResponse> DeleteBasket(DeleteBasketRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new DeleteShoppingBasketCommand(Guid.Parse(request.Id)));

            return new DeleteBasketResponse
            {
                Isvalid = result.IsValid,
                Message = JsonSerializer.Serialize(result.Errors)
            };
        }

        public override async Task<UpdateBasketResponse> UpdateBasket(UpdateBasketRequest request, ServerCallContext context)
        {
            var createCustomerBasketCommand = new UpdateShoppingBasketCommand(
                id: Guid.Parse(request.Id),
                isEnded: request.Isended,
                customerId: Guid.Parse(request.Customerid)
            );

            var result = await _mediator.Send(createCustomerBasketCommand);

            return new UpdateBasketResponse()
            {
                Isvalid = result.IsValid,
                Message = JsonSerializer.Serialize(result.Errors)
            };
        }
        #endregion

        #region ShoppingBasketItem
        public override async Task<GetBasketItemResponse> GetBasketItem(GetBasketItemRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetBasketItemQuery(Guid.Parse(request.Id)));

            if (result == null)
            {
                return new GetBasketItemResponse
                {
                    Item = null
                };
            }

            return new GetBasketItemResponse
            {
                Item = new BasketItem
                {
                    Id = Convert.ToString(result.Id),
                    Name = result.Name,
                    Image = result.Image,
                    Value = Convert.ToDouble(result.Value),
                    Quantity = result.Quantity,
                    Productid = Convert.ToString(result.ProductId),
                    Shoppingbasketid = Convert.ToString(result.ShoppingBasketId)
                }
            };
        }

        public override async Task<GetBasketItemByProductResponse> GetBasketItemByProduct(GetBasketItemByProductRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetBasketItemByProductQuery(Guid.Parse(request.Produtid)));

            if(result == null)
            {
                return new GetBasketItemByProductResponse
                {
                    Item = null
                };
            }

            return new GetBasketItemByProductResponse
            {
                Item = new BasketItem
                {
                    Id = Convert.ToString(result.Id),
                    Name = result.Name,
                    Image= result.Image,
                    Value = Convert.ToDouble(result.Value),
                    Quantity = result.Quantity,
                    Productid = Convert.ToString(result.ProductId),
                    Shoppingbasketid = Convert.ToString(result.ShoppingBasketId)
                }
            };
        }

        public override async Task<AddBasketItemResponse> AddBasketItem(AddBasketItemRequest request, ServerCallContext context)
        {
            var createBasketItemCommand = new UpdateBasketItemCommand(
                id: Guid.Parse(request.Id),
                name: request.Name,
                quantity: request.Quantity,
                value: Convert.ToDecimal(request.Value),
                image: request.Image,
                productId: Guid.Parse(request.Productid),
                shoppingBasketId: Guid.Parse(request.Shoppingbasketid)
            );

            var result = await _mediator.Send(createBasketItemCommand);

            return new AddBasketItemResponse
            {
                Isvalid = result.IsValid,
                Message = JsonSerializer.Serialize(result.Errors)
            };
        }

        public override async Task<RemoveBasketItemResponse> RemoveBasketItem(RemoveBasketItemRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new DeleteBasketItemCommand(Guid.Parse(request.Id), Guid.Parse(request.Basketid)));

            return new RemoveBasketItemResponse
            {
                Isvalid = result.IsValid,
                Message = JsonSerializer.Serialize(result.Errors)
            };
        }
        #endregion
    }
}
