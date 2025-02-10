using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace SokanAcademy.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string[] Tags { get; set; } = Array.Empty<string>();


        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }


        // navigation property for Category
        public Category Category { get; set; }


       
        public ICollection<Comment> Comments { get; set; }= new List<Comment>();

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

    }
}
