using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoesStore.Data;
using ShoesStore.Models;
using ShoesStore.Models.Home;
using ShoesStore.Models.Shoes;
using ShoesStore.Services.Shoes;
using ShoesStore.Services.Statistics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly IShoeService shoes;

        public HomeController(IStatisticsService statistics, IShoeService shoes)
        {
            this.statistics = statistics;
            this.shoes = shoes;
        }

        public IActionResult Index()
        {

            var latestShoes = this.shoes.Latest().ToList();


            var totalStatistics = this.statistics.Total();


            return View(new IndexViewModel
            {

                TotalShoes = totalStatistics.TotalShoes,
                TotalUsers = totalStatistics.TotalUsers,
                Shoes = latestShoes

            });
            


            
        }

      
        public IActionResult Error() => View();
    }
}
