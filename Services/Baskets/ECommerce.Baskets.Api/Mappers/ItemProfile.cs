using AutoMapper;
using ECommerce.Baskets.Application.Commands.Item;
using ECommerce.Baskets.Domain.Models;

namespace ECommerce.Baskets.Api.Mappers
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<ItemCommand, Item>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));
        }
    }
}
