using Microsoft.AspNetCore.Mvc.Rendering;

namespace SokanAcademy.ViewModels
{
    public class EditBlogViewModel
    {
        public int BlogId { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
       public IFormFile? ImageUrl { get; set; } = null;
        public string ImagePath { get; set; } = "";


        public string[] Tags { get; set; } = Array.Empty<string>();

        public int CategoryId { get; set; }
        public string TagsString { get; set; } = "";
        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    }
}
