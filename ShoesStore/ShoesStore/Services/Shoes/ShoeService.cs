﻿using ShoesStore.Data;
using ShoesStore.Data.Models;
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
                .Select(s => new ShoeCategoryServiceModel
                {
                    Id = s.Id,
                    Name = s.Name

                })
                .ToList();
        

        public IEnumerable<ShoeServiceModel> ByUsers(string userId)
        => this.GetShoes(this.data.Shoes.Where(s => s.Seller.UserId == userId));

        public bool CategoryExists(int categoryId)
        => this.data
               .Categories
               .Any(c => c.Id == categoryId);

        public int Create( string brand, string model, int size, string color, string matter, string description, string imageUrl, int categoryId, int sellerId)
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
                CategoryId = categoryId,
                SellerId = sellerId
            };

            this.data.Shoes.Add(shoeData);

            this.data.SaveChanges();

            return shoeData.Id;

        }

        public ShoeDetailsServiceModel Details(int id)
        => this.data
            .Shoes
            .Where(s => s.Id == id)
            .Select(s => new ShoeDetailsServiceModel
            {

                Id = s.Id,
                Brand = s.Brand,
                Model = s.Model,
                Description = s.Description,
                ImageUrl = s.ImageUrl,
                Size = s.Size,
                SellerId = s.SellerId,
                SellerName = s.Seller.Name,
                UserId = s.Seller.UserId
            })
            .FirstOrDefault();

        public bool Edit(int id, string brand, string model, int size, string color, string matter, string description, string imageUrl, int categoryId)
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
            shoeData.CategoryId = categoryId;

            this.data.SaveChanges();

            return true;
        }

        public bool IsBySeller(int shoeId, int sellerId)
        => this.data
               .Shoes
               .Any(s => s.Id == shoeId && s.SellerId == sellerId);
            
        

        private IEnumerable<ShoeServiceModel> GetShoes(IQueryable<Shoe> shoeQuery)
        => shoeQuery
                   .Select(s => new ShoeServiceModel
                   {
                           Id = s.Id,
                           Brand = s.Brand,
                           Model = s.Model,
                           Size = s.Size,
                           Color = s.Color,
                           Matter = s.Matter,
                           ImageUrl = s.ImageUrl
                         
                           
                   })
                    .ToList();


    }
}
