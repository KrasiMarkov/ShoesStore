using ShoesStore.Models.Shoes;
using ShoesStore.Services.Shoes.Models;
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

        IEnumerable<LatestShoeServiceModel> Latest();

        ShoeDetailsServiceModel Details(int shoeId);

        int Create(
                string brand,
                string model,
                int size,
                string color,
                string matter,
                string description,
                string imageUrl,
                int categoryId,
                int sellerId);

        bool Edit(
               int shoeId,
               string brand,
               string model,
               int size,
               string color,
               string matter,
               string description,
               string imageUrl,
               int categoryId);

        bool IsBySeller(int shoeId, int sellerId);

        IEnumerable<string> AllBrands();

        IEnumerable<ShoeServiceModel> ByUsers(string userId);

        IEnumerable<ShoeCategoryServiceModel> AllCategories();

        bool CategoryExists(int categoryId);



    }
}
