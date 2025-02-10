using Microsoft.AspNetCore.Mvc.Rendering;
using SokanAcademy.Models;

namespace SokanAcademy.Data.Repository
{
    public interface IRepository
    {
        IEnumerable<SelectListItem> GetCategories();
        
    }
}
