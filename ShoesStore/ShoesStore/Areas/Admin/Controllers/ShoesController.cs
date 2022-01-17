using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesStore.Services.Shoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ShoesStore.Areas.Admin.AdminConstants;

namespace ShoesStore.Areas.Admin.Controllers
{
   
    public class ShoesController : AdminController
    {
        private readonly IShoeService shoes;

        public ShoesController(IShoeService shoes)
        {
            this.shoes = shoes;

        }

        
        public IActionResult All()
        {
            var shoes = this.shoes
                .All(publicOnly: false)
                .Shoes;

            return View(shoes);
        }

        public IActionResult ChangeVisibility(int id) 
        {
            this.shoes.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
      
    }
}
