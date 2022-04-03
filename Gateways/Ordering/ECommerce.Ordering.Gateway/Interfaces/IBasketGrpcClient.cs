using ECommerce.Basket.Api.Protos;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Interfaces
{
    public interface IBasketGrpcClient
    {
        #region ShoppingBasket
        Task<CreateBasketResponse> CreateShoppingBasket(CreateBasketRequest request);
        Task<GetAllBasketResponse> GetAllShoppingBasket(GetAllBasketRequest request);
        Task<GetBasketByIdResponse> GetShoppingBasketById(GetBasketByIdRequest request);
        Task<GetBasketByCustomerResponse> GetShoppingBasketByCustomer(GetBasketByCustomerRequest request);
        Task<DeleteBasketResponse> DeleteShoppingBasket(DeleteBasketRequest request);
        Task<UpdateBasketResponse> UpdateShoppingBasket(UpdateBasketRequest request);
        #endregion

        #region BasketItem
        Task<GetBasketItemResponse> GetBasketItem(GetBasketItemRequest request);
        Task<GetBasketItemByProductResponse> GetBasketItemByProduct(GetBasketItemByProductRequest request);
        Task<AddBasketItemResponse> AddBasketItem(AddBasketItemRequest request);
        Task<RemoveBasketItemResponse> RemoveBasketItem(RemoveBasketItemRequest request);
        #endregion
    }
}