using E_Commerce_MVC.Areas.Identity.Data;
using E_Commerce_MVC.Services.Abstract;
using E_Commerce_Shared;
using E_Commerce_Shared.DTO;
using E_Commerce_Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_MVC.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly E_Commerce_MVCContext _context;
        public CategoryService(E_Commerce_MVCContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<CategoryDTO>> CreateCategory(CategoryDTO request)
        {
            Category category = new Category()
            {
                CategoryName = request.Name,
            };
            var result = _context.Categories.Add(category);
            if (await _context.SaveChangesAsync() > 0)
            {
                return new ServiceResponse<CategoryDTO>
                {
                    Message = "Operation success",
                    Success = true,
                };
            }
            else
            {
                return new ServiceResponse<CategoryDTO>
                {
                    Message = "Operation is failed",
                    Success = false,
                };
            }
        }

        public async Task<ServiceResponse<bool>> DeleteCategory(int categoryId)
        {
            var result = await _context.Categories.FindAsync(categoryId);
            if (result == null)
            {
                return new ServiceResponse<bool>
                {
                    Message = "Category is not found",
                    Success = false,
                };
            }
            else
            {
                _context.Categories.Remove(result);
                if (await _context.SaveChangesAsync()>0)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = true,
                        Message = "Success",
                    };
                }
                else
                {
                    return new ServiceResponse<bool>
                    {
                        Message = "While data is save to db,create event fail",
                    };
                }
            }
        }

        public List<Category> GetCategories()
        {
            var result = _context.Categories.ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<ServiceResponse<CategoryDTO>> GetCategory(int categoryId)
        {
            var result = await _context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == categoryId);
            CategoryDTO modelDTO = new()
            {
                Name = result.CategoryName,
                Id = result.Id,
            };
            if (result == null)
            {
                return new ServiceResponse<CategoryDTO>
                {
                    Success = true,
                    Message = "This category has not product",
                };
            }
            else
            {
                return new ServiceResponse<CategoryDTO> { Success = true, Data = modelDTO };
            }
        }

        public async Task<Category> GetCategoryByName(string categoryName)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == categoryName);
            if (category != null)
            {
                return category;
            }
            return null;
        }

        public async Task<ServiceResponse<List<Category>>> ListCategory()
        {
            ServiceResponse<List<Category>> serviceResponse = new ServiceResponse<List<Category>>();
            var result = await _context.Categories.ToListAsync();
            if (result.Count == 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Categories are not found";
                return serviceResponse;
            }
            else
            {
                serviceResponse.Success = true;
                serviceResponse.Message = "Categories listed";
                serviceResponse.Data = result;  
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<CategoryDTO>> UpdateCategory(int categoryId, CategoryDTO categoryDTO)
        {
            var result = await _context.Categories.FindAsync(categoryId);
            if (result == null)
            {
                return new ServiceResponse<CategoryDTO>
                {
                    Message = "Ooops! category is not found",
                    Success = false,
                };
            }
            else
            {
                result.CategoryName = categoryDTO.Name;
                await _context.SaveChangesAsync();
                return new ServiceResponse<CategoryDTO>
                {
                    Message = "your process is successfull",
                    Success = true,
                };
            }
        }
    }
}
