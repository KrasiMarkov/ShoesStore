using ShoesStore.Models.Shoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Services.Shoes
{
    public interface IShoeService
    {
        ShoeQueryServiceModel All(string brand,
            string searchTerm,
            ShoesSorting sorting,
            int currentPage,
            int shoesPerPage);

        IEnumerable<string> AllShoeBrands();

        IEnumerable<ShoeServiceModel> ByUsers(string userId);

    }
}
