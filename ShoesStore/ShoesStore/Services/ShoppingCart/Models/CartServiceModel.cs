using ShoesStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Services.ShoppingCart.Models
{
    public class CartServiceModel
    {
        public int Id { get; set; }

        public string CartId { get; set; }

        public int Quantity { get; set; }

        public DateTime DateCreated { get; set; }

        public int ShoeId { get; set; }

        public Shoe Shoe { get; set; }

    }
}
