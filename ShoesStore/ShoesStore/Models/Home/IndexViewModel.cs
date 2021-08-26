using ShoesStore.Services.Shoes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Models.Home
{
    public class IndexViewModel
    {
        public int TotalShoes { get; init; }

        public int TotalUsers { get; init; }

        public int TotalSales { get; init; }

        public IList<LatestShoeServiceModel> Shoes { get; init; }

    }
}
