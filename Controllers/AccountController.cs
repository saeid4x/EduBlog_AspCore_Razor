using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using SokanAcademy.Models;
using SokanAcademy.ViewModels;

namespace SokanAcademy.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            _signInManager= signInManager;
            _userManager= userManager;
            
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        { 


            if(ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = vm.Username,
                    Email = vm.Email,
                    Name = vm.Name
                };

                var result = await _userManager.CreateAsync(user,vm.Password);

                if(result.Succeeded)
                {
                    await _signInManager.SignInAsync(user,isPersistent: false);
                  return   RedirectToAction("Index", "Home");
                }

            }


            
                foreach (var error in ModelState)
                {
                    Console.WriteLine($"---- Key: {error.Key}, Errors: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
                }

            

            return View(vm);    

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("--- model is valid");
               var result = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, false, false);

                if(result.Succeeded)
                {
                    Console.WriteLine("---login Succeeded");
                    return RedirectToAction("Index", "Home");
                }

                Console.WriteLine("---login error");


            }

            foreach (var error in ModelState)
            {
                Console.WriteLine($"---- Key: {error.Key}, Errors: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
            }


            return View(vm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index" , "Home");

        }

       
    }
}
