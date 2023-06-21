using FreeCourse.Services.Catalog.DTOs;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Shared.DTOs;

namespace FreeCourse.Services.Catalog.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDTO>>> GetAllAsync();
        Task<Response<Category>> CreateAsync(CategoryDTO categoryDTO);
        Task<Response<CategoryDTO>> GetByIdAsync(string id);
    }
}
