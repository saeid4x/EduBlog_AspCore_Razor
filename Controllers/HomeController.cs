using Microsoft.AspNetCore.Mvc;
using SokanAcademy.Data.Repository;
using SokanAcademy.ViewModels;
using System.Diagnostics;

namespace SokanAcademy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _repo;
        private readonly ICourseRepository _courseRepo;
        private readonly IPodcastRepository _podcastRepo;
        private readonly IBlogRepository _blogRepo;

        public HomeController(ILogger<HomeController> logger, IBlogRepository blogRepo, ICourseRepository courseRepo , IPodcastRepository podcastRepo, IRepository repo )
        {
            _logger = logger;
            _blogRepo = blogRepo;
            _podcastRepo = podcastRepo;
            _courseRepo = courseRepo;
            _repo=repo;
        }


        
        
        public async Task<IActionResult> Index()
        {
            var categories = _repo.GetCategories();
            var latestBlog = await _blogRepo.GetBlogsAsync();
            latestBlog.OrderByDescending(b=>b.CreatedAt).Take(5).ToList();

            var latestCourse = await _courseRepo.GetAllCourseAsync();
            latestCourse.OrderByDescending(b => b.CreatedAt).Take(5).ToList();

            var latestPodcast = await _podcastRepo.GetAllPodcastsAsync();
            latestCourse.OrderByDescending(b => b.CreatedAt).Take(5).ToList();


            return View(new HomePageViewModel
            {
                Categories = categories,
                LatestCourse=latestCourse,
                LatestPodcast=latestPodcast,
                LatestBlog=latestBlog,
            });
        }

       



    }
}
