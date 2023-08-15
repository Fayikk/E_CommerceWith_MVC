using E_Commerce_Shared;
using E_Commerce_Shared.DTO;

namespace E_Commerce_MVC.Services.Abstract
{
    public interface IStarService
    {
        Task<ServiceResponse<string>> GiveStarProduct(StarDTO model);
    }
}
