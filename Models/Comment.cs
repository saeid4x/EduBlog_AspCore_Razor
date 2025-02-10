using System.ComponentModel.DataAnnotations;

namespace SokanAcademy.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsConfirm { get; set; } = false;


        public int CourseId { get; set; } = 0;
        public Course Course { get; set; }

        public int BlogId { get; set; } = 0;

        // navigation property
        public Blog Blog { get; set; }

        public int PodcastId { get; set; } = 0;
        public Podcast Podcast { get; set; }


    }
}
