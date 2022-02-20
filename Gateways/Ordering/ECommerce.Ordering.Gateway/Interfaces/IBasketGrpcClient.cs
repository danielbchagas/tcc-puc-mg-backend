using ECommerce.Basket.Api.Protos;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Interfaces
{
    public interface IBasketGrpcClient
    {
        #region ShoppingBasket
        Task<CreateBasketResponse> CreateShoppingBasket(CreateBasketRequest request);
        Task<GetBasketResponse> GetShoppingBasketByCustomer(GetBasketByCustomerRequest request);
        Task<DeleteBasketResponse> DeleteShoppingBasket(DeleteBasketRequest request);
        #endregion

        #region BasketItem
        Task<GetBasketItemByProductResponse> GetBasketItemByProduct(GetBasketItemByProductRequest request);
        Task<AddBasketItemResponse> AddBasketItem(AddBasketItemRequest request);
        Task<RemoveBasketItemResponse> RemoveBasketItem(RemoveBasketItemRequest request);
        #endregion
    }
}