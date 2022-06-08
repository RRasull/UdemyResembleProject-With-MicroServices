using FreeCourse.Services.Catalog.DTOs;
using FreeCourse.Services.Catalog.Entities;
using FreeCourse.Shared.DTOs;

namespace FreeCourse.Services.Catalog.Services
{
    public interface ICourseService
    {
        Task<Response<List<CourseDto>>> GetAllAsync();
        Task<Response<CourseDto>> GetByIdAsync(string id);
        Task<Response<CourseDto>> CreateAsync(Course course);
        Task<Response<List<CourseDto>>> GetAllByUserId(string userId)
    }
}
