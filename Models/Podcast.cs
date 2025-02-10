namespace SokanAcademy.Models
{
    public class Podcast
    {
        public int PodcastId { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string JobTitle { get; set; } = "";
        public string VoiceUrl { get; set; } = "";


        // relations 
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
