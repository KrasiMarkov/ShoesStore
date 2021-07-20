﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoesStore.Data;
using ShoesStore.Models;
using ShoesStore.Models.Shoes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShoesStoreDbContext data;

        public HomeController(ShoesStoreDbContext data)
            => this.data = data;

        public IActionResult Index()
        {
            var shoes = this.data
                          .Shoes
                          .OrderByDescending(s => s.Id)
                          .Select(s => new ShoeListingViewModel
                          {
                              Id = s.Id,
                              Brand = s.Brand,
                              Model = s.Model,
                              ImageUrl = s.ImageUrl,
                              Size = s.Size,
                              Category = s.Category.Name
                          })
                          .Take(4)
                          .ToList();

            return View(shoes);


            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
