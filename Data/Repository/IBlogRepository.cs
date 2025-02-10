using SokanAcademy.Models;

namespace SokanAcademy.Data.Repository
{
    public interface IBlogRepository
    {
        Task CreateblogAsync(Blog blog);
        Task<List<Blog>> GetBlogsAsync( );
        Task<List<Blog>> GetBlogsByUserAsync(string UserId);

        Task<Blog> GetBlogAsync(int id);
        Task<Blog> GetBlogByUserAsync(int id,string userId);

        Task RemoveBlogAsync(int id);
        Task EditBlogAsync(Blog blog);
    }
}
