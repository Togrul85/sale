using FrontToBack2.DAL;
using FrontToBack2.Extensions;
using FrontToBack2.Models;
using FrontToBack2.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FrontToBack2.Areas.AdminArea.Controllers
{

    [Area("AdminArea")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View(_appDbContext.Sliders.ToList());
        }
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
      
        public IActionResult  Create  (SliderCreateVM sliderCreateVM)
        {
            if (sliderCreateVM.Photo==null)
            {
                ModelState.AddModelError("Photo", "bosh qoyma");
                return View();

            }
            if (!sliderCreateVM.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "only image");
                return View();
            }   
            if (sliderCreateVM.Photo.CheckImageSize(500))
            {
                ModelState.AddModelError("Photo", "olcusu boyukdur");
                return View();
            }

            //string fileName = Guid.NewGuid().ToString() + sliderCreateVM.Photo.FileName;

            //string fullPath = Path.Combine(_env.WebRootPath,"img", fileName);

            //using (FileStream stream =new FileStream(fullPath, FileMode.Create))
            //    {
            //    sliderCreateVM.Photo.CopyTo(stream);
            //}
           
              
           
            Slider newSlider = new();
            newSlider.ImageUrl = sliderCreateVM.Photo.SaveImage(_env,"img",sliderCreateVM.Photo.FileName);

            _appDbContext.Sliders.Add(newSlider);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult Delete (int id) 
        {
            if (id == null) return NotFound();
            var slider = _appDbContext.Sliders.FirstOrDefault(s=>s.Id== id);
            if (slider == null) return NotFound();
            string fullPath = Path.Combine(_env.WebRootPath,"img",slider.ImageUrl);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            
            _appDbContext.Remove(slider);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
  
        }
        public IActionResult Edit(int id)
        {
            {
                if (id == null) return NotFound();
                Slider slider = _appDbContext.Sliders.SingleOrDefault(c => c.Id == id);
                if (slider == null)
                {
                    return NotFound();
                }
                return View(slider);
            }
        }
    }
}
