using SokanAcademy.Models;

namespace SokanAcademy.Data.Repository
{
    public interface IPodcastRepository
    {
        Task<List<Podcast>> GetAllPodcastsAsync();


        Task<Podcast> GetPodcastAsync(int id);
        Task AddPodcastAsync(Podcast pod);
        Task EditPodcastAsync(Podcast pod);
        Task RemovePodcastAsync(int id);
    }
}
