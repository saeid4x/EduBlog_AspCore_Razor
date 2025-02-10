using SokanAcademy.Models;

namespace SokanAcademy.Data.Repository
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllCourseAsync();
        Task<List<Course>> GetAllCourseByUserAsync(string userId);
        Task AddCourseAsync(Course course);
        Task<Course?> GetCourseAsync(int id);
        Task RemoveCourseAsync(int id);
    }
}
