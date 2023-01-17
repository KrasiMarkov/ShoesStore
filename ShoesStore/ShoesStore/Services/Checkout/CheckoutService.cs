using ShoesStore.Data;
using ShoesStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoesStore.Services.ShoppingCart;

namespace ShoesStore.Services.Checkout
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ShoesStoreDbContext data;

        public CheckoutService(ShoesStoreDbContext data)
        {
            this.data = data;
        }

        public Order CreateOrder(
            string firstName, 
            string lastName, 
            string address, 
            string city, 
            string state, 
            string postalCode, 
            string country, 
            string phone, 
            string email)
        {

            var order = new Order
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                City = city,
                State = state,
                PostalCode = postalCode,
                Country = country,
                Phone = phone,
                Email = email,
                OrderDate = DateTime.Now
            };

            this.data.Orders.Add(order);

            this.data.SaveChanges();

            return order;


        }

        
    }
}
