using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Services.Shoes
{
    public class ShoeDetailsServiceModel : ShoeServiceModel
    {
        public string Description { get; set; }

        public int SellerId { get; set; }

        public string SellerName { get; set; }

        public int CategoryId { get; set; }

        public string UserId { get; set; }


    }
}
