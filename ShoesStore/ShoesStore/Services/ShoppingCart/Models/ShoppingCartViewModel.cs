using ShoesStore.Data.Models;
using ShoesStore.Services.Shoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Services.ShoppingCart.Models
{
    public class ShoppingCartViewModel
    {
        public List<CartServiceModel> CartItems { get; set; }

        public decimal CartTotal { get; set; }

        

    }
}
