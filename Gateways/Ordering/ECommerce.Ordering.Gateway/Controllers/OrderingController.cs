using ECommerce.Ordering.Gateway.Constants;
using ECommerce.Ordering.Gateway.DTOs.Request;
using ECommerce.Ordering.Gateway.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderingController : ControllerBase
    {
        private readonly IOrderingGrpcClient _orderingGrpcClient;
        private readonly IBasketGrpcClient _basketGrpcClient;
        private readonly ICustomerGrpcClient _customerGrpcClient;

        public OrderingController(IBasketGrpcClient basketGrpcClient, IOrderingGrpcClient orderingGrpcClient, ICustomerGrpcClient customerGrpcClient)
        {
            _orderingGrpcClient = orderingGrpcClient;
            _basketGrpcClient = basketGrpcClient;
            _customerGrpcClient = customerGrpcClient;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _orderingGrpcClient.GetOrder(new Api.Protos.GetOrderRequest
            {
                Id = Convert.ToString(id)
            });

            if(result.Order == null)
                return NotFound(ResponseMessages.OrderNotFound);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Create(OrderingRequest order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)));

            var basket = (await _basketGrpcClient.GetShoppingBasketByCustomer(new Basket.Api.Protos.GetBasketByCustomerRequest
            {
                Customerid = Convert.ToString(order.CustomerId)
            })).Basket;

            if (basket == null)
                return BadRequest(ResponseMessages.BasketNotFound);

            var customer = (await _customerGrpcClient.GetCustomer(new Customer.Api.Protos.GetUserRequest
            {
                Id = basket.Customerid
            })).User;

            if (customer == null)
                return BadRequest(ResponseMessages.CustomerNotFound);

            if (customer.Document == null || customer.Email == null || customer.Phone == null || customer.Address == null)
                return BadRequest(ResponseMessages.CustomerInformationsNotFound);

            var newOrder = new Api.Protos.CreateOrderRequest
            {
                Id = Convert.ToString(order.Id == Guid.Empty ? Guid.NewGuid() : order.Id),
                Fullname = $"{customer.Firstname} {customer.Lastname}",
                Document = customer.Document.Number,
                Email = customer.Email.Address,
                Phone = customer.Phone.Number,
                Firstline = customer.Address.Firstline,
                Secondline = customer.Address.Secondline,
                City = customer.Address.City,
                State = customer.Address.State,
                Zipcode = customer.Address.Zipcode,
                Value = basket.Value,
            };

            foreach (var item in basket.Items)
            {
                newOrder.Items.Add(new Api.Protos.OrderItem
                {
                    Id = Convert.ToString(Guid.NewGuid()),
                    Name = item.Name,
                    Image = item.Image,
                    Value = item.Value,
                    Quantity = item.Quantity,
                    Productid = item.Productid,
                    Orderid = newOrder.Id
                });
            }

            var createOrderResult = await _orderingGrpcClient.CreateOrder(newOrder);

            if (!createOrderResult.Isvalid)
                return BadRequest(ResponseMessages.CreateOrderFail);

            var updateBasketResult = await _basketGrpcClient.UpdateShoppingBasket(new Basket.Api.Protos.UpdateBasketRequest
            {
                Id = basket.Id,
                Isended = true,
                Customerid = basket.Customerid
            });

            if(!updateBasketResult.Isvalid)
                return BadRequest(ResponseMessages.UpdateBasketFail);

            return Ok(newOrder);
        }
    }
}
