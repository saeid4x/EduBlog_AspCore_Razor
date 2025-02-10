using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SokanAcademy.Models;

namespace SokanAcademy.Data.Repository
{
    public class Repository : IRepository
    {
        private AppDbContext _context;
        public Repository(AppDbContext context)
        {

            _context = context; 

        }
        public IEnumerable<SelectListItem> GetCategories()
        {
            var cats = _context.Categories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.Title
            });
            return cats;
        }
    }
}
