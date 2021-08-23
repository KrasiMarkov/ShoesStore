using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoesStore.Data;
using ShoesStore.Models;
using ShoesStore.Models.Home;
using ShoesStore.Models.Shoes;
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
        private readonly IMapper mapper;
        private readonly ShoesStoreDbContext data;

        public HomeController(IStatisticsService statistics, ShoesStoreDbContext data, IMapper mapper)
        {
            this.statistics = statistics;
            this.data = data;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
          
            var shoes = this.data
                          .Shoes
                          .OrderByDescending(s => s.Id)
                          .ProjectTo<ShoeIndexViewModel>(this.mapper.ConfigurationProvider)
                          .Take(8)
                          .ToList();


            var totalStatistics = this.statistics.Total();


            return View(new IndexViewModel
            {
                
                TotalShoes = totalStatistics.TotalShoes,
                TotalUsers = totalStatistics.TotalUsers,
                Shoes = shoes

            });
            


            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
