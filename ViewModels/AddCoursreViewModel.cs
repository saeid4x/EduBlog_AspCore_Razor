using Microsoft.AspNetCore.Mvc.Rendering;
using SokanAcademy.Models;

namespace SokanAcademy.ViewModels
{
    public class AddCoursreViewModel
    {
       
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public ContentType ContentType { get; set; } = ContentType.Video;
        public Level Lavel { get; set; } = Level.Intro;
        public string Duration { get; set; } = "";
        public IFormFile ImageUrl { get; set; } = null;



        public ICollection<ChapterViewModel> Chapters { get; set; } = new List<ChapterViewModel>();
        //   public ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        // foreign key
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        // Navigation property 
        // public Category Category { get; set; }

        //  public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
