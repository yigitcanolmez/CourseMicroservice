using FreeCourse.Services.Catalog.DTOs;
using FreeCourse.Shared.DTOs;

namespace FreeCourse.Services.Catalog.Services.CourseServices
{
    public interface ICourseService
    {
        Task<Response<List<CourseDTO>>> GetAllAsync();

        Task<Response<CourseDTO>> GetByIdAsync(string id);

        Task<Response<List<CourseDTO>>> GetAllByUserId(string id);
        Task<Response<CourseDTO>> CreateAsync(CourseCreateDTO courseDto);

        Task<Response<NoContent>> UpdateAsync(CourseUpdateDTO courseDto);

        Task<Response<NoContent>> DeleteAsync(string Id);
    }
}