using ShoesStore.Services.Shoes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Models.Shoes
{
    public class AllShoesQueryModel
    {
        public const int ShoesPerPage = 4;

        public string Brand { get; init; }

        public IEnumerable<string> Brands { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; init; }

        public ShoesSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalShoes { get; set; }

        public IEnumerable<ShoeServiceModel> Shoes { get; set; }

    }
}
