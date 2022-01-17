using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using ShoesStore.Data;
using ShoesStore.Models;

using ShoesStore.Models.Shoes;
using ShoesStore.Services.Shoes;
using ShoesStore.Services.Shoes.Models;
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
       
        private readonly IShoeService shoes;
        private readonly IMemoryCache cache;

        public HomeController(IShoeService shoes, IMemoryCache cache)
        {
            this.shoes = shoes;
            this.cache = cache;
        }

        public IActionResult Index()
        {

            const string latestShoesCacheKey = "LatestShoesCasheKey";

            var latestShoes = this.cache.Get<List<LatestShoeServiceModel>>(latestShoesCacheKey);

            

            if (latestShoes == null)
            {
                 latestShoes = this.shoes.Latest().ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(latestShoesCacheKey, latestShoes, cacheOptions);

            }

            return View(latestShoes);
            
        }

      
        public IActionResult Error() => View();
    }
}
