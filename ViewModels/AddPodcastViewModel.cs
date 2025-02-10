using Microsoft.AspNetCore.Mvc.Rendering;

namespace SokanAcademy.ViewModels
{
    public class AddPodcastViewModel
    {
        
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
       
        public string JobTitle { get; set; } = "";
        public IFormFile VoiceUrl { get; set; } = null;


         
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    }
}
