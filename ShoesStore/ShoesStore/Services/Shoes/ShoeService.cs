using ShoesStore.Data;
using ShoesStore.Models.Shoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Services.Shoes
{
    public class ShoeService : IShoeService
    {
        private readonly ShoesStoreDbContext data;

        public ShoeService(ShoesStoreDbContext data) 
            => this.data = data;

        public ShoeQueryServiceModel All(string brand, 
            string searchTerm, 
            ShoesSorting sorting, 
            int currentPage,
            int shoesPerPage)
        {
            var shoesQuery = this.data.Shoes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                shoesQuery = shoesQuery.Where(s => s.Brand == brand);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                shoesQuery = shoesQuery.Where(s =>
                       (s.Brand + " " + s.Model).ToLower().Contains(searchTerm.ToLower()) ||
                       s.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            shoesQuery = sorting switch
            {
                ShoesSorting.DateCreated => shoesQuery.OrderByDescending(s => s.Id),
                ShoesSorting.Size => shoesQuery.OrderByDescending(s => s.Size),
                ShoesSorting.BrandAndModel => shoesQuery.OrderBy(s => s.Brand).ThenBy(s => s.Model),
                _ => shoesQuery.OrderByDescending(s => s.Id)
            };


            var totalShoes = shoesQuery.Count();

            var shoes = shoesQuery
                            .Skip((currentPage - 1) * shoesPerPage)
                            .Take(shoesPerPage)
                            .Select(s => new ShoeServiceModel
                            {
                                Id = s.Id,
                                Brand = s.Brand,
                                Model = s.Model,
                                ImageUrl = s.ImageUrl,
                                Size = s.Size,
                                Category = s.Category.Name
                            })
                            .ToList();


            return new ShoeQueryServiceModel
            {
                TotalShoes = totalShoes,
                CurrentPage = currentPage,
                ShoesPerPage = shoesPerPage,
                Shoes = shoes

            };
        }

        public IEnumerable<string> AllShoeBrands() 
                   => this.data
                          .Shoes
                          .Select(s => s.Brand)
                          .Distinct()
                          .OrderBy(br => br)
                          .ToList();
    }
}
