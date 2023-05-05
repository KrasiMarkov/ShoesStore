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
        ShoeQueryServiceModel All(
            string brand = null,
            string searchTerm = null,
            ShoesSorting sorting = ShoesSorting.DateCreated,
            int currentPage = 1,
            int shoesPerPage = int.MaxValue,
            bool publicOnly = true);

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
                decimal price,
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
               decimal price,
               int categoryId,
               bool isPublic);

        bool Delete(int id);

        bool IsBySeller(int shoeId, int sellerId);

        void ChangeVisibility(int shoeId);

        IEnumerable<string> AllBrands();

        IEnumerable<ShoeServiceModel> ByUsers(string userId);

        IEnumerable<ShoeCategoryServiceModel> AllCategories();

        bool CategoryExists(int categoryId);

        public ShoeServiceModel GetShoeById(int id);


    }
}
