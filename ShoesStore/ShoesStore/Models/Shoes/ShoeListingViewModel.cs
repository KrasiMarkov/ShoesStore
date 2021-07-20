using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Models.Shoes
{
    public class ShoeListingViewModel
    {
        public int Id { get; init; }

        public string Brand { get; init; }

        public string Model { get; init; }

        public string ImageUrl { get; init; }

        public int Size { get; init; }

        public string Category { get; init; }

    }
}
