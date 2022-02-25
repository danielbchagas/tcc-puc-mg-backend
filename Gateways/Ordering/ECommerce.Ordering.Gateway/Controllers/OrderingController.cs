using ECommerce.Ordering.Gateway.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [HttpPost]
        public async Task<IActionResult> Create(Guid customerId)
        {
            var basket = await GetBasket(customerId);

            if (basket == null)
                return BadRequest("Carrinho de compras não encontrado.");

            var customer = await GetUser(Guid.Parse(basket.Customerid));

            if (customer == null)
                return BadRequest("Cliente não encontrado.");

            if (customer.Document == null || customer.Email == null || customer.Phone == null || customer.Address == null)
                return BadRequest("Verifique seus dados cadastrais.");

            var order = new Api.Protos.CreateOrderRequest
            {
                Id = Convert.ToString(Guid.NewGuid()),
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
                order.Items.Add(new Api.Protos.OrderItem
                {
                    Id = Convert.ToString(Guid.NewGuid()),
                    Name = item.Name,
                    Image = item.Image,
                    Value = item.Value,
                    Quantity = item.Quantity,
                    Productid = item.Productid,
                    Orderid = order.Id
                });
            }

            var response = await _orderingGrpcClient.CreateOrder(order);

            if (!response.Isvalid)
                return BadRequest("Falha ao criar o pedido.");

            return Ok();
        }

        #region Helpers
        private async Task<Basket.Api.Protos.ShoppingBasket> GetBasket(Guid customerId)
        {
            var basketResponse = await _basketGrpcClient.GetShoppingBasketByCustomer(new Basket.Api.Protos.GetBasketByCustomerRequest
            {
                Customerid = Convert.ToString(customerId)
            });

            return basketResponse.Basket;
        }

        private async Task<Customer.Api.Protos.User> GetUser(Guid id)
        {
            var customerResponse = await _customerGrpcClient.GetCustomer(new Customer.Api.Protos.GetUserRequest
            {
                Id = Convert.ToString(id)
            });

            return customerResponse.User;
        }
        #endregion
    }
}
