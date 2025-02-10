using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SokanAcademy.Data.Repository;
using SokanAcademy.Models;
using SokanAcademy.ViewModels;
using System.Security.Claims;

namespace SokanAcademy.Controllers
{
    //[Route("panel/podcast")]
    public class PodcastController : Controller
    {
        private readonly IPodcastRepository _podcastRepo;
        private readonly IRepository _repo;
        public PodcastController(IPodcastRepository podcastRepo, IRepository repo)
        {
            _podcastRepo = podcastRepo;
            _repo = repo;
        }


        [HttpGet("podcasts")]
        public async Task<IActionResult> Index(string SearchQuery , string order = "asc", int categoryId = 0)
        {
           var podcasts =   await _podcastRepo.GetAllPodcastsAsync();


            if (!string.IsNullOrEmpty(SearchQuery))
            {
                podcasts = podcasts.Where(c => c.Title.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (categoryId > 0)
            {
                podcasts = podcasts.Where(c => c.CategoryId == categoryId).ToList();
            }

            podcasts = order.ToLower() == "desc" ?
                    podcasts.OrderByDescending(c => c.CreatedAt).ToList() :
                    podcasts.OrderBy(c => c.CreatedAt).ToList();


            // Fetch categories for the dropdown
            ViewBag.Categories = _repo.GetCategories();

            return View("PodcastSearch",podcasts);
        }


        [Authorize]
        [HttpGet("panel/podcasts")]
        public async Task<IActionResult> IndexPanel()
        {
            var podcasts = await _podcastRepo.GetAllPodcastsAsync();


            return View("Index",podcasts);
        }


        [Authorize]
        [HttpGet("panel/podcasts/add")]
        public IActionResult AddPodcast()
        {
            var categories = _repo.GetCategories();
            return View(new AddPodcastViewModel
            {
                Categories=categories
            });
        }

        [Authorize]
        [HttpPost("panel/podcasts/add")]
        public async Task<IActionResult> AddPodcast(AddPodcastViewModel vm)
        {
           

            if (ModelState.IsValid)
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/podcast/voice");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{vm.VoiceUrl.FileName}";

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.VoiceUrl.CopyToAsync(stream);
                }

                string voiceUrl = $"/uploads/podcast/voice/{uniqueFileName}";



                var podcast = new Podcast
                {
                    Title = vm.Title,
                    Content = vm.Content,
                    VoiceUrl = voiceUrl,
                    CategoryId = vm.CategoryId,
                    JobTitle = vm.JobTitle,
                    UserId=currentUserId
                };

                Console.WriteLine($"addpodcast:voiceurl= {podcast.VoiceUrl}");

                await _podcastRepo.AddPodcastAsync(podcast);
                return RedirectToAction(nameof(IndexPanel));
            }

            foreach (var error in ModelState)
            {
                Console.WriteLine($"---- Key: {error.Key}, Errors: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
            }

            return NotFound();
          


        }

        [HttpGet("podcasts/{id:int}")]
        public async Task<IActionResult> ViewPodcast([FromRoute] int id)
        {
            var podcast = await _podcastRepo.GetPodcastAsync(id);

            return View(podcast);

        }


        [Authorize]
        [HttpGet("panel/podcasts/{id:int}/remove")]
        public async  Task<IActionResult> RemovePodcast([FromRoute] int id)
        {
            await _podcastRepo.RemovePodcastAsync(id);
            return RedirectToAction(nameof(IndexPanel));
        }



        [Authorize]
        [HttpGet("panel/podcasts/{id:int}/edit")]
        public async Task<IActionResult> EditPodcast([FromRoute] int id)
        {
            var podcast = await _podcastRepo.GetPodcastAsync(id);
            var categories = _repo.GetCategories();

            var vm = new EditPodcastViewModel
            {
                PodcastId = podcast.PodcastId,
                Title = podcast.Title,
                Content = podcast.Content,
                VoiceUrl = podcast.VoiceUrl,
                JobTitle = podcast.JobTitle,
                CategoryId = podcast.CategoryId,
                Categories = categories
            };

            return View(vm);

        }


        [Authorize]
        [HttpPost("panel/podcasts/{id:int}/edit")]
        public async  Task<IActionResult> EditPodcast(EditPodcastViewModel vm)
        {
            string voicePath = "";
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState)
                {
                    Console.WriteLine($"---- Key: {error.Key}, Errors: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");

                    Console.WriteLine($"error-voice url= {vm.VoiceUrl}");
                }

                return NotFound();
            }

            if(vm.VoiceUrlFile !=null && vm.VoiceUrlFile.Length > 0)
            {
                // handle voice file

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/podcast/voice");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{vm.VoiceUrlFile.FileName}";

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.VoiceUrlFile.CopyToAsync(stream);
                }

                  voicePath = $"/uploads/podcast/voice/{uniqueFileName}";
            }

            Console.WriteLine($"success-voice url= {vm.VoiceUrl}");

            var podcast = new Podcast
            {
                PodcastId = vm.PodcastId,
                Title = vm.Title,
                Content = vm.Content,
                VoiceUrl = string.IsNullOrEmpty(voicePath) ? vm.VoiceUrl : voicePath,
                JobTitle = vm.JobTitle,
                CategoryId = vm.CategoryId,
                UserId= currentUserId
            };

            await _podcastRepo.EditPodcastAsync(podcast);
            return RedirectToAction(nameof(IndexPanel));
        }
    }
}
