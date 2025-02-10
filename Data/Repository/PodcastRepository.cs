using Microsoft.EntityFrameworkCore;
using SokanAcademy.Models;

namespace SokanAcademy.Data.Repository
{
    public class PodcastRepository : IPodcastRepository
    {
        private readonly AppDbContext _context;

        public PodcastRepository(AppDbContext context)
        {
            
            _context = context;
        }
        public async Task AddPodcastAsync(Podcast pod)
        {
             await _context.Podcasts.AddAsync(pod);
            await _context.SaveChangesAsync();
        }

        public async Task EditPodcastAsync(Podcast pod)
        {
            Console.WriteLine("--- EditPodcastAsync start...");
            var podcast = await _context.Podcasts.FindAsync(pod.PodcastId);
            if(podcast != null)
            {
                podcast.Title = pod.Title;
                podcast.Content = pod.Content;
                podcast.VoiceUrl = pod.VoiceUrl;
                podcast.JobTitle = pod.JobTitle;
                podcast.CategoryId = pod.CategoryId;

                //   _context.Podcasts.Update(podcast);

                var result = await _context.SaveChangesAsync();
                Console.WriteLine($"--- Rows affected: {result}");


            }
        }

        public async  Task<List<Podcast>> GetAllPodcastsAsync()
        {
            return await _context.Podcasts.Include(p => p.Category).Include(p => p.User).ToListAsync();
        }

        public async Task<List<Podcast>> GetAllPodcastsByUserAsync(string userId)
        {
            return await _context.Podcasts.Include(p => p.Category).Include(p => p.User).ToListAsync();
        }

        public async Task<Podcast> GetPodcastAsync(int id)
        {
            var podcast = await _context.Podcasts.Include(p => p.Category).Include(p => p.User).SingleOrDefaultAsync(p => p.PodcastId == id);
            if(podcast!= null)
            {
                return podcast;
            }

            return new Podcast();
        }

        public async Task RemovePodcastAsync(int id)
        {
            var podcast = await GetPodcastAsync(id);
            _context.Podcasts.Remove(podcast);
            await _context.SaveChangesAsync();
        }
    }
}
