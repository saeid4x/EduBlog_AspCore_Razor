using SokanAcademy.Models;

namespace SokanAcademy.Data.Repository
{
    public class SeedRepository : ISeedRepository
    {
        private AppDbContext _context;

        public SeedRepository(AppDbContext context)
        {
            _context = context;
            
        }
        public async Task CreateCategory(Category cat)
        {
            await _context.Categories.AddAsync(cat);
            await _context.SaveChangesAsync();
        }
    }
}
