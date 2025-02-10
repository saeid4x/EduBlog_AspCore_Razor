using Microsoft.AspNetCore.Mvc.Rendering;
using SokanAcademy.Models;

namespace SokanAcademy.ViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        public List<Blog>   LatestBlog { get; set; } = new List<Blog>();
        public List<Course> LatestCourse { get; set; } = new List<Course>();
        public List<Podcast> LatestPodcast { get; set; } = new List<Podcast>();

    }
}
