using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesStore.Data;
using ShoesStore.Data.Infrastructure;
using ShoesStore.Data.Models;
using ShoesStore.Models.Sellers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Controllers
{
    public class SellersController : Controller
    {
        private readonly ShoesStoreDbContext data;

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

            this.data.Add(sellerData);
            this.data.SaveChanges();

            return RedirectToAction("All", "Shoes");

        }

    }
}
