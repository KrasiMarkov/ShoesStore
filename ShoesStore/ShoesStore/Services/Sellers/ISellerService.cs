using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Services.Sellers
{
    public  interface ISellerService
    {
        public bool IsSeller(string userId);


    }
}
