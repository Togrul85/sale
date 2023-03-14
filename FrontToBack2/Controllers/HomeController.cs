using FrontToBack2.DAL;
using FrontToBack2.Models;
using FrontToBack2.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FrontToBack2.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM();

            homeVM.SliderDetail = _appDbContext.SliderDetails.FirstOrDefault();
            homeVM.Categories = _appDbContext.Categories.ToList();
            homeVM.Products = _appDbContext.Products.ToList();
            return  View(homeVM);
        }

    }
}
