using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesStore.Data.Models;
using ShoesStore.Models.Orders;
using ShoesStore.Services.Checkout;
using ShoesStore.Services.Shoes;
using ShoesStore.Services.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ShoesStore.WebConstants;

namespace ShoesStore.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IShoppingCartService shoppingCart;
        private readonly IShoeService shoe;
        private readonly ICheckoutService checkout;
        const string PromoCode = "FREE";

        public CheckoutController(IShoppingCartService shoppingCart, IShoeService shoe, ICheckoutService checkout)
        {
            this.shoppingCart = shoppingCart;
            this.shoe = shoe;
            this.checkout = checkout;
        }

        public IActionResult AddressAndPayment() 
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddressAndPayment(OrderFormModel orderModel)
        {

            if (!ModelState.IsValid)
            {
                return View(orderModel);
            }

            var order = this.checkout.CreateOrder(
                orderModel.FirstName,
                orderModel.LastName,
                orderModel.Address,
                orderModel.City,
                orderModel.State,
                orderModel.PostalCode,
                orderModel.Country,
                orderModel.Phone,
                orderModel.Email);

            int orderDetails = this.shoppingCart.CreateOrderDetails(order);

            TempData[GlobalMessageKey] = $"Your order is successfully created!";

            return RedirectToAction("All", "Shoes");



            //try
            //{
            //    if (string.Equals(model["PromoCode"], PromoCode,
            //        StringComparison.OrdinalIgnoreCase) == false)
            //    {
            //        return View(order);
            //    }
            //    else 
            //    {
            //        order.Username = User.Identity.Name;

            //        order.OrderDate = DateTime.Now;


            //    }
            //}
            //catch 
            //{
            //    return View();
            //}

           
        }

    }
}
