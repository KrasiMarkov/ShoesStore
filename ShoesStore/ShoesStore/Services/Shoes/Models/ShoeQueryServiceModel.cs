using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Services.Shoes
{
    public class ShoeQueryServiceModel
    {
        public int CurrentPage { get; init; } 

        public int ShoesPerPage { get; init; }

        public int TotalShoes { get; init; }

        public IEnumerable<ShoeServiceModel> Shoes { get; init; }

    }
}
