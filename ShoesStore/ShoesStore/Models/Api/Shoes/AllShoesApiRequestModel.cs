using ShoesStore.Models.Shoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Models.Api.Shoes
{
    public class AllShoesApiRequestModel
    {

        public string Brand { get; init; }

        public string SearchTerm { get; init; }

        public ShoesSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int CarsPerPage { get; init; } = 10;
    }
}
