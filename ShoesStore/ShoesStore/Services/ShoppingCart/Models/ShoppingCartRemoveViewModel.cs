using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Services.ShoppingCart.Models
{
    public class ShoppingCartRemoveViewModel
    {
        public string Message { get; set; }

        public decimal CartTotal { get; set; }

        public int CartQuantity { get; set; }

        public int ItemQuantity { get; set; }

        public int DeleteId { get; set; }

    }
}
