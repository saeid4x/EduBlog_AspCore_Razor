using Microsoft.EntityFrameworkCore;
using SokanAcademy.Models;

namespace SokanAcademy.Data.Repository
{
    public class CourseRepository : ICourseRepository
    {
        
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
                  
        }
        public async Task AddCourseAsync(Course course)
        {
              await  _context.Courses.AddAsync(course);
              await _context.SaveChangesAsync();
        }

        public async Task<List<Course>> GetAllCourseAsync()
        {
            return await _context.Courses.Include(c=>c.Category).Include(c=>c.User).ToListAsync();
        }

        public async Task<List<Course>> GetAllCourseByUserAsync(string userId)
        {
            return await _context.Courses.Include(c => c.Category).Include(c => c.User).Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<Course?> GetCourseAsync(int id)
        {
            return await _context.Courses.Include(c => c.Chapters).ThenInclude(ch => ch.ChapterItems)
                                 .Include(c => c.Category).Include(c => c.User)
                                 .FirstOrDefaultAsync(c => c.CourseId == id);
        }

        

        public async Task RemoveCourseAsync(int id)
        {
           var course = await GetCourseAsync(id);
            if(course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }
    }
}
