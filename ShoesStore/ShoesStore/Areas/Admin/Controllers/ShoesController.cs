using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ShoesStore.Areas.Admin.AdminConstants;

namespace ShoesStore.Areas.Admin.Controllers
{
   
    public class ShoesController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }

      
    }
}
