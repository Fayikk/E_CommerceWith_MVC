using AutoMapper;
using E_Commerce_MVC.Areas.Identity.Data;
using E_Commerce_MVC.Services.Abstract;
using E_Commerce_Shared;
using E_Commerce_Shared.DTO;
using E_Commerce_Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_MVC.Services.Concrete
{
    public class FavouriteService : IFavouriteService
    {
        private readonly E_Commerce_MVCContext _context;
        private readonly IMapper _mapper;
        public FavouriteService(E_Commerce_MVCContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

     

        public async Task<bool> AddMyFavourite(FavouriteDTO model)
        {
            var addedObj = _mapper.Map<Favourite>(model);
            _context.Favourites.Add(addedObj);
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteMyFavourite(int favouriteId)
        {
            var deleteObj = await _context.Favourites.FindAsync(favouriteId);
            if (deleteObj != null)
            {
                _context.Favourites.Remove(deleteObj);
                await _context.SaveChangesAsync();
                return true;    
            }
            return false;
        }

        public async Task<ServiceResponse<List<Favourite>>> GetAllMyFavourite(string userId)
        {
            var myFavorites = await _context.Favourites.Where(x => x.UserId == userId).ToListAsync();
            ServiceResponse<List<Favourite>> _response = new ServiceResponse<List<Favourite>>();
            if (myFavorites != null)
            {
                _response.Success = true;
                _response.Data = myFavorites;
                return _response;
            }
            return null;
        }
    }
}
