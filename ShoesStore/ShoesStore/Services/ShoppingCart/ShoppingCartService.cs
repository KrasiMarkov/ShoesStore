using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesStore.Data;
using ShoesStore.Data.Models;
using ShoesStore.Services.Shoes;
using ShoesStore.Services.ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace ShoesStore.Services.ShoppingCart
{
    public partial class ShoppingCartService : IShoppingCartService
    {
        private readonly ShoesStoreDbContext data;
        private readonly IHttpContextAccessor httpContextAccessor;

        public const string CartSessionKey = "CartId";


        public ShoppingCartService(ShoesStoreDbContext data, IHttpContextAccessor httpContextAccessor)
        {
            this.data = data;
            this.httpContextAccessor = httpContextAccessor;
            this.ShoppingCartId = this.GetCartId();
        }

        string ShoppingCartId { get; set; }

        public void AddToCart(ShoeServiceModel shoe)
        {
            var cartItem = data.Carts.SingleOrDefault(s => s.CartId == ShoppingCartId && s.ShoeId == shoe.Id);

            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    ShoeId = shoe.Id,
                    CartId = ShoppingCartId,
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };

                data.Carts.Add(cartItem);
            }

            else 
            {
                cartItem.Quantity++;
            }

            data.SaveChanges();
        }

        public int CreateOrderDetails(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ShoeId = item.ShoeId,
                    OrderId = order.Id,
                    UnitPrice = item.Shoe.Price,
                    Quantity = item.Quantity
                };

                orderTotal += (item.Quantity * item.Shoe.Price);

                this.data.OrderDetails.Add(orderDetail);
            }

            order.Total = orderTotal;

            this.data.SaveChanges();

            EmptyCart();

            return order.Id;
        }

        public void EmptyCart()
        {
            var cartItems = data.Carts.Where(cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                data.Carts.Remove(cartItem);
            }

            data.SaveChanges();
        }

        public List<CartServiceModel> GetCartItems()
        {
            var items = data
                           .Carts
                           .Where(cart => cart.CartId == ShoppingCartId)
                           .Select(c => new CartServiceModel 
                           {
                               Id = c.Id,
                               CartId = c.CartId, 
                               Quantity = c.Quantity, 
                               DateCreated = c.DateCreated,
                               ShoeId = c.ShoeId,
                               Shoe = c.Shoe
                           })
                           .ToList();

            return items;
        }

        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in data.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Quantity).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            // Multiply shoes price by count of that shoes to get 
            // the current price for each of those shoes in the cart
            // sum all shoes price totals to get the cart total
            decimal? total = (from cartItems in data.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Quantity *
                              cartItems.Shoe.Price).Sum();

            return total ?? decimal.Zero;
        }

        public int RemoveFromCart(int id)
        {
            var cardItem = data.Carts.Single(cart => cart.CartId == ShoppingCartId && cart.ShoeId == id);

            int itemQuantity = 0;

            if (cardItem != null) 
            {
                if (cardItem.Quantity > 1)
                {
                    cardItem.Quantity--;
                    itemQuantity = cardItem.Quantity;
                }

                else 
                {
                    data.Carts.Remove(cardItem);
                }

                data.SaveChanges();
            }

            return itemQuantity;

        }

        public string GetCartId()
        {
            HttpContext context = this.httpContextAccessor.HttpContext;

            if (context.Session.GetString(CartSessionKey) == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User?.Identity?.Name))
                {

                    context.Session.SetString(CartSessionKey, context.User.Identity.Name);
                        
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session.SetString(CartSessionKey, tempCartId.ToString());
                }
            }

            return context.Session.GetString(CartSessionKey).ToString();
        }

        public string GetItem(int id)
         => this.data
                     .Carts
                     .Single(item => item.Id == id).Shoe.Model;
                     
                     
        
    }
}
