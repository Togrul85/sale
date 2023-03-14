using FrontToBack2.DAL;
using FrontToBack2.Models;
using FrontToBack2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontToBack2.Controllers
{
    public class BasketController : Controller
    {

        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;

        public BasketController(UserManager<AppUser> userManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }

       
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async IActionResult Sale()
        {
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await  _userManager.FindByNameAsync(User.Identity.Name);

                Sales sale = new();
                sale.AppUserId= user.Id;
                sale.CreatedDate= DateTime.Now;
                //sale.SalesProducts
                List<SalesProducts> salesProduct = new();
                List<BasketVM> basketsVMs =
                    JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
                foreach (var basketProduct in basketsVMs)
                {
                    Product product = _appDbContext.Products.FirstOrDefault(p => p.Id == basketProduct.Id);
                    SalesProducts salesProducts = new();
                    salesProduct.ProductId = product.Id;

                }

            }
            else
            {
                return RedirectToAction("login","account");
            }
            return View();
        }
    }
}
