using AutoMapper;
using E_Commerce_MVC.Models;
using E_Commerce_Shared.DTO;
using E_Commerce_Shared.Entity;

namespace E_Commerce_MVC.MappingProcess
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order,OrderDTO>().ReverseMap();   
            CreateMap<OrderDTO,OrderCheckoutModel>().ReverseMap();
            CreateMap<Star, StarDTO>().ReverseMap();
            CreateMap<Favourite, FavouriteDTO>().ReverseMap();
            CreateMap<Discount, DiscountDTO>().ReverseMap();

        }
    }
}
