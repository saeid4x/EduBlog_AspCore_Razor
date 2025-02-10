using SokanAcademy.Models;

namespace SokanAcademy.ViewModels
{
    public class ViewCourseViewModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public ContentType ContentType { get; set; }
        public Level Lavel { get; set; }
        public string Duration { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public string CategoryName { get; set; } = "";
        public DateTime CreatedAt { get; set; }

        public Category Category;

        public string  UserId { get; set; }
        public ApplicationUser User { get; set; }
        public List<ChapterViewModel> Chapters { get; set; } = new List<ChapterViewModel>();
    }

    
}
