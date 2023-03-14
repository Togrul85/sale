using FrontToBack2.Helpers;
using FrontToBack2.Models;
using FrontToBack2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FrontToBack2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

       
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = new();
            user.Email = register.Email;
            user.Fullname = register.Fullname;
            user.UserName = register.Username;
            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(register);
            }

            //add role 

            _userManager.AddToRoleAsync(user, RoleEnum.Admin.ToString());

           return RedirectToAction("login");






            return RedirectToAction("index", "home");

        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByEmailAsync(login.UsernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(login.UsernameOrEmail);
                if (user == null)
                {

                    ModelState.AddModelError("", "username or email invalid");
                    return View(login);
                }
            }

        var result = await    _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "hesabiniz bloklanib");
                return View(login);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "usernameoremail or password invalid");
                return View(login);
            }



            //sign in

            await _signInManager.SignInAsync(user, true);
















            return RedirectToActionPermanent("index", "home");
        }
        public async Task<IActionResult> Logout()
        {
         await   _signInManager.SignOutAsync();
            return RedirectToAction("login");
        }

        //public async Task<IActionResult> CreateRole()
        //{

        //    foreach (var item in Enum.GetValues(typeof(RoleEnum)))
        //    {
        //        if (_roleManager.RoleExistsAsync(item.ToString)
        //        {

        //        }
        //    }



        //    return Content("role added");
        //}
    }
}
