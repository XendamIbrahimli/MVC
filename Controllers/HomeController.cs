﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using UniqloMVC.DataAccess;
using UniqloMVC.ViewModel;

namespace UniqloMVC.Controllers
{
    public class HomeController(UniqloDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var datas = await _context.Sliders
                .Where(x=>!x.IsDeleted)
                .Select(x=>new SliderItemVM
            {
                ImageUrl = x.ImageUrl,
                Link=x.Link,
                Subtitle=x.Subtitle,
                Title=x.Title
            }).ToListAsync();
            return View(datas);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
