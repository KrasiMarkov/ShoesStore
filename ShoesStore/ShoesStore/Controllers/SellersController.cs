using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesStore.Data;
using ShoesStore.Data.Infrastructure.Extensions;
using ShoesStore.Data.Models;
using ShoesStore.Models.Sellers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ShoesStore.WebConstants;

namespace ShoesStore.Controllers
{
    public class SellersController : Controller
    {
        private readonly ShoesStoreDbContext data;

        public SellersController(ShoesStoreDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        public IActionResult Become() 
            => View();



        [Authorize]
        [HttpPost]
        public IActionResult Become(BecomeSellerFormModel seller)
        {
            var userId = this.User.GetId();


            var userIsAlreadySeller = this.data
                .Sellers
                .Any(s => s.UserId == userId);

            if (userIsAlreadySeller)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(seller);
            }

            var sellerData = new Seller
            {
                Name = seller.Name,
                PhoneNumber = seller.PhoneNumber,
                UserId = userId
            };

            this.data.Sellers.Add(sellerData);
            this.data.SaveChanges();

            this.TempData[GlobalMessageKey] = "Thank you from becoming seller!";

            return RedirectToAction(nameof(ShoesController.All), "Shoes");

        }

    }
}
