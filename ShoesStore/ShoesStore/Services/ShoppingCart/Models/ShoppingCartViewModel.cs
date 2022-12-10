using ShoesStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Services.ShoppingCart.Models
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }

        public decimal CartTotal { get; set; }

    }
}
