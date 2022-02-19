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
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBasketGrpcClient _basketGrpcClient;
        
        public BasketController(IBasketService basketService, IHttpContextAccessor httpContextAccessor, IBasketGrpcClient basketGrpcClient)
        {
            _basketService = basketService;
            _httpContextAccessor = httpContextAccessor;
            _basketGrpcClient = basketGrpcClient;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{customerId:Guid}")]
        public async Task<IActionResult> Get(Guid customerId)
        {
            var proto = await _basketGrpcClient.GetShoppingBasketByCustomer(customerId);
            
            if(proto.Basket == null)
            {
                var basket = new ECommerce.Basket.Api.Protos.CreateBasketRequest
                {
                    Id = Convert.ToString(Guid.NewGuid()),
                    Customerid = Convert.ToString(customerId)
                };

                var _response = await _basketGrpcClient.CreateShoppingBasket(basket);

                if (!_response.Isvalid)
                    return BadRequest(_response.Message);

                return Ok(basket);
            }

            return Ok(proto.Basket);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("basket/{customerId:Guid}")]
        public async Task<IActionResult> Delete(Guid customerId)
        {
            var response = await _basketGrpcClient.DeleteShoppingBasket(customerId);

            if (!response.Isvalid)
                return BadRequest(response.Message);

            return NoContent();
        }
    }
}
