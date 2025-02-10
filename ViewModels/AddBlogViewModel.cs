using Microsoft.AspNetCore.Mvc.Rendering;
using SokanAcademy.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SokanAcademy.ViewModels
{
    public class AddBlogViewModel
    {
        
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public IFormFile ImageUrl { get; set; } =null;
        
        public string[] Tags { get; set; } = Array.Empty<string>();
        
        public int CategoryId { get; set; }
        public string TagsString { get; set; } = "";
        public IEnumerable<SelectListItem> Categories  { get; set; } = new List<SelectListItem>();
      






    }
}
