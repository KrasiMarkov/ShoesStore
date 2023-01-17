using Microsoft.AspNetCore.Mvc;
using ShoesStore.Services.Shoes;
using ShoesStore.Services.ShoppingCart;
using ShoesStore.Services.ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace ShoesStore.Controllers
{
    public class ShoppingCartController : Controller
    {

        private readonly IShoppingCartService shoppingCart;
        private readonly IShoeService shoe;
        public ShoppingCartController(IShoppingCartService shoppingCart, IShoeService shoe)
        {
            this.shoppingCart = shoppingCart;
            this.shoe = shoe;
        }
         
        public IActionResult Index()
        {
            var cart = shoppingCart;

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(viewModel);
        }

        public IActionResult AddToCart(int id)
        {
            var addedShoe = this.shoe.GetShoeById(id);

            var cart = shoppingCart;

            cart.AddToCart(addedShoe);

            return RedirectToAction("Index");
        }

        
        public IActionResult RemoveFromCart(int id)
        {
            var cart = shoppingCart;

            //string shoeName = cart.GetItem(id);

            int itemQuantity = cart.RemoveFromCart(id);

            //var results = new ShoppingCartRemoveViewModel()
            //{
            //    CartTotal = cart.GetTotal(),
            //    CartQuantity = cart.GetCount(),
            //    ItemQuantity = itemQuantity,
            //    DeleteId = id
            //};

            //return json(results);

            return RedirectToAction("Index");
        }
    }
}
