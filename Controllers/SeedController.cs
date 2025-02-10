using Microsoft.AspNetCore.Mvc;
using SokanAcademy.Data.Repository;
using SokanAcademy.Models;

namespace SokanAcademy.Controllers
{
    
    public class SeedController : Controller
    {
        private ISeedRepository _repo;
        public SeedController(ISeedRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("seed/category")]
        public IActionResult AddCategory()
        {
            return View(new Category());
        }


        [HttpPost("seed/category")]
        public  async Task<IActionResult> AddCategory(Category cat)
        {
            await _repo.CreateCategory(cat);
            return View(new Category());
        }





        


    }
}
