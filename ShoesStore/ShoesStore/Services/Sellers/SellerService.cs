using ShoesStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Services.Sellers
{
    public class SellerService : ISellerService
    {
        private readonly ShoesStoreDbContext data;

        public SellerService(ShoesStoreDbContext data)
        {
            this.data = data;
        }

        public bool IsSeller(string userId)
        {
            return this.data
                       .Sellers
                       .Any(s => s.UserId == userId);
                       
        }
    }
}
