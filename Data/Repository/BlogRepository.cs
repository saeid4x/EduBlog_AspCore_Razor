using Microsoft.EntityFrameworkCore;
using SokanAcademy.Models;

namespace SokanAcademy.Data.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private AppDbContext _context;
        public BlogRepository(AppDbContext context)
        {

            _context = context;

        }
        public async Task CreateblogAsync(Blog blog)
        {
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();            
        }

        public async Task EditBlogAsync(Blog updatedBlog)
        {
             var blog = await _context.Blogs.FindAsync(updatedBlog.BlogId);
          
            if (blog != null)
            {
                blog.BlogId= updatedBlog.BlogId;
                blog.Title= updatedBlog.Title;
                blog.Content=updatedBlog.Content;
                blog.CategoryId=updatedBlog.CategoryId;
                blog.ImageUrl = updatedBlog.ImageUrl;
                 blog.Tags=updatedBlog.Tags;
                blog.UserId=updatedBlog.UserId;
                await _context.SaveChangesAsync();
            }
            
        }

        public async Task<Blog> GetBlogAsync(int id)
        {
            var blog = await _context.Blogs.Include(b=>b.Category).Include(b=>b.User).FirstOrDefaultAsync(b=> b.BlogId== id) ;
            if(blog != null)
            {
                return blog;
            }
            return new Blog();
        }

        public async Task<Blog> GetBlogByUserAsync(int id, string userId)
        {
            var blog = await _context.Blogs.Include(b => b.Category).FirstOrDefaultAsync(b => b.BlogId == id && b.UserId == userId);
            if (blog != null)
            {
                return blog;
            }
            return new Blog();
        }

      

        public async Task<List<Blog>> GetBlogsAsync( )
        {
            var blogs = await _context.Blogs.Include(b=>b.Category).Include(b=>b.User).ToListAsync();
            if(blogs != null)
            {
                return blogs;
            }
            return new List<Blog>();
        }

        public async Task<List<Blog>> GetBlogsByUserAsync(string userId)
        {
            var blogs = await _context.Blogs.Include(b => b.Category).Include(b => b.User).Where(b => b.UserId == userId).ToListAsync();


            if (blogs != null)
            {
                return blogs;
            }
            return new List<Blog>();
        }

        public async Task RemoveBlogAsync(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if(blog != null )
            {
                  _context.Blogs.Remove(blog);
                await _context.SaveChangesAsync();
            }
        }
    }
}
