using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Models.Shoes
{
    public class AllShoesQueryModel
    {
        public string Brand { get; init; }

        public IEnumerable<string> Brands { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; init; }

        public ShoesSorting Sorting { get; init; }

        public IEnumerable<ShoeListingViewModel> Shoes { get; set; }

    }
}
