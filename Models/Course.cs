namespace SokanAcademy.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public ContentType ContentType { get; set; } = ContentType.Video;
        public Level Lavel { get; set; } = Level.Intro;
        public string Duration { get; set; } = "";
        public string ImageUrl { get; set; } = "";

      

        public ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        // foreign key
        public int  CategoryId { get; set; }

        // Navigation property 
        public Category Category { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }

    public enum ContentType
    {
        Text,
        Video,
        Text_Video,
        Voice
    }

    public enum Level
    {
        Intro,
        Middle,
        Advance
    }
}
