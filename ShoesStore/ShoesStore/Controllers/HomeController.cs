using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoesStore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
