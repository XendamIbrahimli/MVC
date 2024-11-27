﻿using Microsoft.AspNetCore.Mvc;
using UniqloMVC.DataAccess;
using UniqloMVC.Models;
using UniqloMVC.ViewModels.Slider;

namespace UniqloMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController(UniqloDbContext _context, IWebHostEnvironment _env) : Controller
    {
        public IActionResult Index()
        {
            _context.Sliders.ToList();
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SliderCreateVM vm)
        {
            if (!vm.File.ContentType.Contains("image"))
                ModelState.AddModelError("File","File type must be image");
            if(vm.File.Length > 600 * 1024)
             ModelState.AddModelError("File", "File length must be less than 600kb");
            if (!ModelState.IsValid) return View();
             
            string newFileName=Path.GetRandomFileName()+Path.GetExtension(vm.File.FileName);

            
            
            using(Stream stream = System.IO.File.Create(Path.Combine(_env.WebRootPath, "imgs","sliders",newFileName)))
            {
               await vm.File.CopyToAsync(stream);
                
            }

            Slider slider = new Slider
            {
                ImageUrl = newFileName,
                Link = vm.Link,
                Subtitle = vm.Subtitle,
                Title = vm.Title,
            };
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

           
            //return View();

        }
    }
}