using E_Commerce_Shared;
using E_Commerce_Shared.DTO;
using E_Commerce_Shared.Entity;

namespace E_Commerce_MVC.Services.Abstract
{
    public interface IFavouriteService
    {
        Task<bool> AddMyFavourite(FavouriteDTO model);
        Task<bool> DeleteMyFavourite(int favouriteId);
        Task<ServiceResponse<List<Favourite>>> GetAllMyFavourite(string userId);
     
    }
}
