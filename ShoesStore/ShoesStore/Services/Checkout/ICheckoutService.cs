using ShoesStore.Data.Models;
using ShoesStore.Services.ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Services.Checkout
{
    public interface ICheckoutService
    {
        Order CreateOrder(
                string firstName,
                string lastName,
                string address,
                string city,
                string state,
                string postalCode,
                string country,
                string phone,
                string email);

       

    }
}
