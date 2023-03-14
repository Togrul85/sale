﻿using FrontToBack2.DAL;
using FrontToBack2.Models;
using FrontToBack2.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FrontToBack2.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View(_appDbContext.Categories.ToList());
        }
        public IActionResult Detail(int id)
        {
           
            {
                if (id == null) return NotFound();
                Category category= _appDbContext.Categories.SingleOrDefault(c => c.Id==id);
                if (category ==null)
                {
                    return NotFound();
                }
                return View(category);
            } 
        }




        //public IActionResult Delete(int id)
        //{
        //    if(id==null) return NotFound();

        //    Category category = _appDbContext.Categories.SingleOrDefault(c => c.Id == id);

        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(category);

        //}

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public IActionResult Create(CategoryCreateVM category)
        {
            if (!ModelState.IsValid) return View();
            
            bool isExist  = _appDbContext.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower());
            if (isExist)
                ModelState.AddModelError("Name", "Bu adli c movcuddur");
            {
                return View();
            }
            Category newCategory = new()
            {
                Name = category.Name,
                Description = category.Description
            };
            _appDbContext.Categories.Add(newCategory);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
            //return Content($"{category.Name} {category.Description}");
        }



        public IActionResult Edit(int id)
        {
            if (id == null) return NotFound();

            Category category = _appDbContext.Categories.SingleOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }
            return View(new CategoryUpdateVM { Name=category.Name,Description=category.Description});

        }
        [HttpPost]
        public  IActionResult Edit( int id,CategoryUpdateVM updateVM)
        {
            if (id == null) return NotFound();


            Category existCategory = _appDbContext.Categories.Find(id);
            if (!ModelState.IsValid) return View();

            bool isExist = _appDbContext.Categories.Any(c => c.Name.ToLower() == updateVM.Name.ToLower()&&c.Id!=id);
            if (isExist)
                ModelState.AddModelError("Name", "Bu adli c movcuddur");
            {
                return View();
            }
            if (existCategory == null)
            {
     return NotFound();
            }
            existCategory.Name= updateVM.Name;
            existCategory.Description= updateVM.Description;
            _appDbContext.SaveChanges();
            
            return RedirectToAction("Index");

        }
      
        public IActionResult Delete(int id)
        {
            if (id == null) return NotFound();

            Category category = _appDbContext.Categories.SingleOrDefault(c => c.Id == id);

            if (category == null)
            {
    return NotFound();
            }
            _appDbContext.Categories.Remove(category);
            return RedirectToAction("Index");

        }



    }
}

