using Microsoft.AspNetCore.Mvc.Rendering;

namespace SokanAcademy.ViewModels
{
    public class EditPodcastViewModel
    {
        public int PodcastId { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string JobTitle { get; set; } = "";
        public string VoiceUrl { get; set; } = "";
        public IFormFile? VoiceUrlFile { get; set; } = null;
        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();




        public int CategoryId { get; set; }
    }
}
