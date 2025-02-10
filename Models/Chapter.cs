using System.ComponentModel.DataAnnotations;

namespace SokanAcademy.Models
{
    public class Chapter
    {
        [Key]
        public int ChapterId { get; set; }

        public string Title { get; set; } = "";
        public string Duration { get; set; } = "";

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public ICollection<ChapterItem> ChapterItems { get; set; } = new List<ChapterItem>();



    }
}
