using Microsoft.AspNetCore.Mvc;

namespace FrontToBack2.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
