using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SokanAcademy.Data.Repository;
using SokanAcademy.Models;
using SokanAcademy.ViewModels;
using System.CodeDom;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace SokanAcademy.Controllers
{
    // [Route("panel/course")]
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepo;
        private readonly IRepository _repo;

        public CourseController(ICourseRepository courseRepo, IRepository repo)
        {
            _courseRepo = courseRepo;
            _repo = repo;

        }

        [HttpGet("courses")]
        public async  Task<IActionResult> Index(string SearchQuery ,   string level,string contentType  , string order = "asc", int categoryId = 0)
        {
            var courses = await _courseRepo.GetAllCourseAsync();

            if (!string.IsNullOrEmpty(SearchQuery))
            {
               courses= courses.Where(c => c.Title.Contains(SearchQuery,StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if(categoryId > 0)
            {
               courses= courses.Where(c => c.CategoryId == categoryId).ToList();    
            }

            /*
             * 
             Used Enum.TryParse to convert the level string into the corresponding Level enum.
             If parsing succeeds, it filters courses where c.Lavel == selectedLevel.  
            
             * Enum.TryParse<TEnum>: Safely converts a string to an enum. The true parameter makes the comparison case-insensitive.
            

             */
            if (!string.IsNullOrEmpty(level) && Enum.TryParse<Level>(level, true, out var selectedLevel))
            {
                Console.WriteLine($"--- level= {level} \t | selectedLevel= {selectedLevel}");
                courses = courses.Where(c => c.Lavel == selectedLevel).ToList();
            }

            if (!string.IsNullOrEmpty(contentType) && Enum.TryParse<ContentType>(contentType, true, out var selectedContentType))
            {
                courses = courses.Where(c => c.ContentType == selectedContentType).ToList();
            }

            courses = order.ToLower() == "desc" ?
                       courses.OrderByDescending(c => c.CreatedAt).ToList() :
                       courses.OrderBy(c => c.CreatedAt).ToList();



            // Fetch categories for the dropdown
            ViewBag.Categories = _repo.GetCategories();

            return View("CourseSearch", courses);
        }

        [HttpGet("panel/courses")]
        public async Task<IActionResult> IndexPanel()
        {
            var courses = await _courseRepo.GetAllCourseAsync();
            ViewBag.Categories = _repo.GetCategories();
            return View("Index",courses);

        }

        [Authorize]
        [HttpGet("panel/courses/add")]
        public IActionResult AddCourse()
        {
            var categories =   _repo.GetCategories();


            return View(new AddCoursreViewModel
            {
                Categories=categories
            });

        }



        [Authorize]
        [HttpPost("panel/courses/add")]
        public async Task<IActionResult> AddCourse(AddCoursreViewModel vm)
        {

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(!ModelState.IsValid)
            {
                Console.WriteLine("--- model is invalid");
                foreach(var error in ModelState)
                {
                    Console.WriteLine($"Key:{error.Key}\t , Errors:{string.Join(',',
                        error.Value.Errors.Select(e=>e.ErrorMessage))}" );
                }
                //--------
                vm.Categories=_repo.GetCategories();
                return View(vm);
            }

            // handle image file
            string imagePath = "";
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/course");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueImageName = $"{Guid.NewGuid()}_{vm.ImageUrl.FileName}";

            imagePath = $"/uploads/course/{uniqueImageName}";
            var filePath = Path.Combine(uploadsFolder, uniqueImageName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await vm.ImageUrl.CopyToAsync(stream);
            }


            var course = new Course
            {
                Title = vm.Title,
                Content = vm.Content,
                ImageUrl = imagePath ?? "no-image.jpg",
                Lavel = vm.Lavel,
                ContentType = vm.ContentType,
                CategoryId = vm.CategoryId,
                Duration = vm.Duration,
                UserId=currentUserId,
                
            };

            foreach(var chapterVm in vm.Chapters)
            {
                var chapter = new Chapter
                {
                    Title = chapterVm.Title,
                    Duration = chapterVm.Duration
                };

                foreach(var itemVm in chapterVm.ChapterItems)
                {
                    chapter.ChapterItems.Add(new ChapterItem
                    {
                        Title = itemVm.Title,
                        Content = itemVm.Content,
                    });
                }

                course.Chapters.Add(chapter);
            }

            await _courseRepo.AddCourseAsync(course);


            return RedirectToAction(nameof(IndexPanel));
        }

        [HttpGet("courses/{id:int}")]
        public async Task<IActionResult> ViewCourse([FromRoute] int id)
        {
            var course = await _courseRepo.GetCourseAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            var vm = new ViewCourseViewModel {
                CourseId = course.CourseId,
                Title = course.Title,
                Content = course.Content,
                ContentType = course.ContentType,
                Lavel = course.Lavel,
                Duration = course.Duration,
                ImageUrl = course.ImageUrl,
                CategoryName = course.Category?.Title ?? "Uncategorized",
                CreatedAt = course.CreatedAt,
                Category = course.Category,
                User=course.User,
                Chapters = course.Chapters.Select(chapter => new ChapterViewModel
                {
                    ChapterId = chapter.ChapterId,
                    Title = chapter.Title,
                    Duration = chapter.Duration,
                    ChapterItems = chapter.ChapterItems.Select(item => new ChapterItemViewModel
                    {
                        ChapterItemId = item.ChapterItemId,
                        Title = item.Title,
                        Content = item.Content
                    }).ToList()
                }).ToList(),
                
        };
            return View(vm);
        }


        [Authorize]
        [HttpGet("panel/courses/{id:int}/remove")]
        public async Task<IActionResult> RemoveCourse([FromRoute] int id)
        {
            await _courseRepo.RemoveCourseAsync(id);
            return RedirectToAction(nameof(IndexPanel));
        }
    }
}
