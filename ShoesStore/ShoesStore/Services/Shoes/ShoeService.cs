using AutoMapper;
using AutoMapper.QueryableExtensions;
using ShoesStore.Data;
using ShoesStore.Data.Models;
using ShoesStore.Models.Shoes;
using ShoesStore.Services.Shoes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Services.Shoes
{
    public class ShoeService : IShoeService
    {
        private readonly ShoesStoreDbContext data;
        private readonly IMapper mapper;

        public ShoeService(ShoesStoreDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public ShoeQueryServiceModel All(
            string brand = null, 
            string searchTerm = null, 
            ShoesSorting sorting = ShoesSorting.DateCreated, 
            int currentPage = 1,
            int shoesPerPage = int.MaxValue,
            bool publicOnly = true)
        {
            var shoesQuery = this.data.Shoes.Where(s => !publicOnly || s.IsPublic);

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

            var shoes = GetShoes(shoesQuery
                            .Skip((currentPage - 1) * shoesPerPage)
                            .Take(shoesPerPage));
                            


            return new ShoeQueryServiceModel
            {
                TotalShoes = totalShoes,
                CurrentPage = currentPage,
                ShoesPerPage = shoesPerPage,
                Shoes = shoes

            };
        }

        public IEnumerable<string> AllBrands() 
                   => this.data
                          .Shoes
                          .Select(s => s.Brand)
                          .Distinct()
                          .OrderBy(br => br)
                          .ToList();

        public IEnumerable<ShoeCategoryServiceModel> AllCategories()
        
           => this.data
                .Categories
                .ProjectTo<ShoeCategoryServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();
        

        public IEnumerable<ShoeServiceModel> ByUsers(string userId)
        => this.GetShoes(this.data.Shoes.Where(s => s.Seller.UserId == userId));

        public bool CategoryExists(int categoryId)
        => this.data
               .Categories
               .Any(c => c.Id == categoryId);

        public void ChangeVisibility(int shoeId)
        {
            var shoe = this.data.Shoes.Find(shoeId);

            shoe.IsPublic = !shoe.IsPublic;

            this.data.SaveChanges();
        }

        public int Create( string brand, string model, int size, string color, string matter, string description, string imageUrl, decimal price, int categoryId, int sellerId)
        {

            var shoeData = new Shoe
            {
                Brand = brand,
                Model = model,
                Size = size,
                Color = color,
                Matter = matter,
                Description = description,
                ImageUrl = imageUrl,
                Price = price,
                CategoryId = categoryId,
                SellerId = sellerId,
                IsPublic = false
            };

            this.data.Shoes.Add(shoeData);

            this.data.SaveChanges();

            return shoeData.Id;

        }

        public ShoeDetailsServiceModel Details(int id)
        => this.data
            .Shoes
            .Where(s => s.Id == id)
            .ProjectTo<ShoeDetailsServiceModel>(this.mapper.ConfigurationProvider)
            .FirstOrDefault();

        public bool Edit(int id, string brand, string model, int size, string color, string matter, string description, string imageUrl, decimal price, int categoryId, bool isPublic)
        {
            var shoeData = this.data.Shoes.Find(id);

            if (shoeData == null)
            {
                return false;
            }
           
            shoeData.Brand = brand;
            shoeData.Model = model;
            shoeData.Size = size;
            shoeData.Color = color;
            shoeData.Matter = matter;
            shoeData.Description = description;
            shoeData.ImageUrl = imageUrl;
            shoeData.Price = price;
            shoeData.CategoryId = categoryId;
            shoeData.IsPublic = isPublic;

            this.data.SaveChanges();

            return true;
        }

        public bool IsBySeller(int shoeId, int sellerId)
        => this.data
               .Shoes
               .Any(s => s.Id == shoeId && s.SellerId == sellerId);


        public IEnumerable<LatestShoeServiceModel> Latest()
        => this.data
                     .Shoes
                     .Where(s => s.IsPublic)
                     .OrderByDescending(s => s.Id)
                     .ProjectTo<LatestShoeServiceModel>(this.mapper.ConfigurationProvider)
                     .Take(3)
                     .ToList();


        private IEnumerable<ShoeServiceModel> GetShoes(IQueryable<Shoe> shoeQuery)
        => shoeQuery
                   .ProjectTo<ShoeServiceModel>(this.mapper.ConfigurationProvider)
                    .ToList();

        public ShoeServiceModel GetShoeById(int id)
         =>   this.data
                       .Shoes
                       .Where(s => s.Id == id)
                       .ProjectTo<ShoeServiceModel>(this.mapper.ConfigurationProvider)
                       .FirstOrDefault();
    }
}
