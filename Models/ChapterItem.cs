using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SokanAcademy.Models
{
    public class ChapterItem
    {
        [Key]
        public int ChapterItemId { get; set; }

        public string Title { get; set; } = "";
        public string Content { get; set; } = "";

        [ForeignKey(nameof(Chapter))]
        public int ChapterId { get; set; }

        public Chapter Chapter { get; set; }
    }
}
