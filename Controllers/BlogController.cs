using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SokanAcademy.Data.Repository;
using SokanAcademy.Models;
using SokanAcademy.ViewModels;
using System.Security.Claims;

namespace SokanAcademy.Controllers
{
     
    public class BlogController : Controller
    {
        private IBlogRepository _blogRepo;
        private IRepository _repo;


        public BlogController(IBlogRepository blogRepo, IRepository repo)
        {
            _blogRepo = blogRepo;
            _repo = repo;

        }

        [Authorize]
        [HttpGet("panel/blogs")]
        public async  Task<IActionResult> IndexPanel()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(!string.IsNullOrEmpty(currentUserId))
            {
            // get list of all blogs
            var blogs = await _blogRepo.GetBlogsByUserAsync(currentUserId);

            return View("Index",blogs);

            }

            return RedirectToAction("Login", "Account");

        }

       

        //[HttpGet("panel/blogs")]
        [HttpGet("blogs")]
        public async Task<IActionResult> Index(string SearchQuery, int categoryId=0,string order="asc")
        {
          


            // get list of all blogs
            var blogs = await _blogRepo.GetBlogsAsync();

            // Apply filters if search parameters are provided
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                blogs= blogs.Where(b=>b.Title.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (categoryId >0)
            {
                blogs = blogs.Where(b => b.CategoryId == categoryId).ToList();
            }


            // Order by creation date
            blogs = order.ToLower() == "desc"
                ? blogs.OrderByDescending(b => b.CreatedAt).ToList()
                : blogs.OrderBy(b => b.CreatedAt).ToList();

            // Fetch categories for the dropdown
            ViewBag.Categories =   _repo.GetCategories();
            return View("BlogSearch",blogs);
        }

        [HttpGet("blogs/{id:int}")]
        public async Task<IActionResult> ViewBlog([FromRoute] int id)
        {
            var blog= await _blogRepo.GetBlogAsync(id);
            return View(blog);
        }


        [Authorize]
        [HttpGet("panel/blogs/add")]
        public IActionResult AddBlog()
        {
            // list categories
            var categories = _repo.GetCategories();

            return View(new AddBlogViewModel
            {
                Categories = categories
            });

           
        }


        [Authorize]
        [HttpPost("panel/blogs/add")]
        public async Task<IActionResult> AddBlog(AddBlogViewModel vm)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(ModelState.IsValid && !string.IsNullOrEmpty(currentUserId))
            {
                string imagePath = null;
                // handle image upload 
                if (vm.ImageUrl != null && vm.ImageUrl.Length > 0)
                {
                    /*
                     * output of uploadsFolder=
                     * D:\temp download\temp_project\dotnet\AspNetProject2\SokanAcademy\wwwroot/uploads/blog/images
                     * 
                     */
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/blog/images");

                    Directory.CreateDirectory(uploadsFolder);

                    /*
                     * uniqueFileName=
                     *  3b578948-e052-45c7-b2ce-7faff06bed9e_8c.jpg
                     */
                    var uniqueFileName = $"{Guid.NewGuid()}_{vm.ImageUrl.FileName}";


                    /*
                     * filePath=
                     * D:\temp download\temp_project\dotnet\AspNetProject2\SokanAcademy\wwwroot/uploads/blog/images\3b578948-e052-45c7-b2ce-7faff06bed9e_8c.jpg 
                     */
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    Console.WriteLine($"--- file path: {filePath} || uploadas folder: {uploadsFolder} || uniqueFileName: {uniqueFileName} ");

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {

                        await vm.ImageUrl.CopyToAsync(stream);
                    }

                    imagePath = $"/uploads/blog/images/{uniqueFileName}";

                }




                var blog = new Blog
                {
                    Title = vm.Title,
                    Content = vm.Content,
                    ImageUrl = imagePath ?? "no-image.jpg",
                    CreatedAt = DateTime.Now,
                    Tags = vm.TagsString.Split(',').Select(s => s.Trim()).ToArray(),
                    CategoryId = vm.CategoryId,
                    UserId = currentUserId
                };
                await _blogRepo.CreateblogAsync(blog);
                return RedirectToAction("IndexPanel");



            }

            foreach (var error in ModelState)
            {
                Console.WriteLine($"---- Key: {error.Key}, Errors: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
            }



            return View(vm);

        }

        [Authorize]
        [HttpGet("panel/blog/edit/{id:int}")]
        public async Task<IActionResult> EditBlog([FromRoute] int id)
        {
            var blog = await _blogRepo.GetBlogAsync(id);
            // list categories
            var categories = _repo.GetCategories();
            if (blog != null)
            {
                var vm = new EditBlogViewModel
                {
                    BlogId=blog.BlogId,
                    Title = blog.Title,
                    Content = blog.Content,
                    CategoryId = blog.CategoryId,
                    TagsString = string.Join(", ", blog.Tags),
                    Categories = categories,
                    ImagePath=blog.ImageUrl

                };

                return View(vm);

            }

            return View();

        }


        [Authorize]
        [HttpPost("panel/blog/edit/{id:int}")]
        public async Task<IActionResult> EditBlog(EditBlogViewModel vm)
        {

            var currnetUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState)
                {
                    Console.WriteLine($"---- Key: {error.Key}, Errors: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
                }

            }


           if (ModelState.IsValid)
            {
                string[] tags = vm.TagsString.Split(',').Select(t => t.Trim()).ToArray();
                string imagePath = "";

                // handle image upload 
                if(vm.ImageUrl != null && vm.ImageUrl.Length > 0)
                {

                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/blog/images");

                    var uniqeFileName = $"{Guid.NewGuid()}_{vm.ImageUrl.FileName}";

                    var filePath = Path.Combine(uploadsFolder, uniqeFileName);

                    // If a previous image exists, delete it
                    if (!string.IsNullOrEmpty(vm.ImagePath))
                    {
                        var previousImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", vm.ImagePath.TrimStart('/'));
                        Console.WriteLine($"--- imgae path: {vm.ImagePath} ");
                        if (System.IO.File.Exists(previousImagePath))
                        {
                            System.IO.File.Delete(previousImagePath);
                        }
                    }



                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await vm.ImageUrl.CopyToAsync(stream);
                    }

                    imagePath = $"/uploads/blog/images/{uniqeFileName}";

                }


                var updatedBlog = new Blog
                {
                    BlogId = vm.BlogId,
                    Title = vm.Title,
                    Content = vm.Content,
                    ImageUrl = string.IsNullOrEmpty(imagePath) ? vm.ImagePath : imagePath,
                    CategoryId = vm.CategoryId,
                    Tags = tags,
                    UserId = currnetUserId,
                };

                await _blogRepo.EditBlogAsync(updatedBlog);
                return RedirectToAction("IndexPanel");

            }

            // list categories
            var categories = _repo.GetCategories();
            vm.Categories = categories;
            return View(vm);

        }

        [Authorize]
        [HttpGet("/panel/blogs/{id:int}/remove")]
        public async Task<IActionResult> RemoveBlog([FromRoute] int id)
        {
            await _blogRepo.RemoveBlogAsync(id);
            return RedirectToAction("IndexPanel");
        }


        
    }
}
