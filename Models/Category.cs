namespace SokanAcademy.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Title { get; set; } = "";

        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
        public ICollection<Course> Courses { get; set; } = new List<Course>();



    }
}
