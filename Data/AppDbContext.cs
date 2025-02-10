using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
 
using SokanAcademy.Models;

namespace SokanAcademy.Data
{
    public class AppDbContext :  IdentityDbContext<ApplicationUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }



        public DbSet<Course> Courses { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<ChapterItem> ChapterItems { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Podcast> Podcasts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comment>()
                .HasOne(b => b.Blog)
                .WithMany(c=> c.Comments)
                .HasForeignKey(c =>c.BlogId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Course)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.CourseId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Podcast)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.PodcastId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
