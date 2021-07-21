﻿using Microsoft.AspNetCore.Mvc;
using ShoesStore.Data;
using ShoesStore.Data.Models;
using ShoesStore.Models.Shoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Controllers
{
    public class ShoesController : Controller
    {
        private readonly ShoesStoreDbContext data;

        public ShoesController(ShoesStoreDbContext data) 
            => this.data = data;

        public IActionResult Add() => View(new AddShoeFormModel
        {
            Categories = this.GetShoeCategories()

        });

        public IActionResult All([FromQuery] AllShoesQueryModel query)
        {
            var shoesQuery = this.data.Shoes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Brand))
            {
                shoesQuery = shoesQuery.Where(s => s.Brand == query.Brand);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                shoesQuery = shoesQuery.Where(s => 
                       (s.Brand + " " + s.Model).ToLower().Contains(query.SearchTerm.ToLower()) ||
                       s.Description.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            shoesQuery = query.Sorting switch
            {
                ShoesSorting.DateCreated => shoesQuery.OrderByDescending(s => s.Id),
                ShoesSorting.Size => shoesQuery.OrderByDescending(s => s.Size),
                ShoesSorting.BrandAndModel => shoesQuery.OrderBy(s => s.Brand).ThenBy(s => s.Model),
                _ => shoesQuery.OrderByDescending(s => s.Id)
            };


            var shoes = shoesQuery
                            .Skip((query.CurrentPage - 1) * AllShoesQueryModel.ShoesPerPage)
                            .Take(AllShoesQueryModel.ShoesPerPage)
                            .Select(s => new ShoeListingViewModel
                            {
                                Id = s.Id,
                                Brand = s.Brand,
                                Model = s.Model,
                                ImageUrl = s.ImageUrl,
                                Size = s.Size,
                                Category = s.Category.Name
                            })
                            .ToList();

            var shoesBrands = this.data
                                 .Shoes
                                 .Select(s => s.Brand)
                                 .Distinct()
                                 .OrderBy(br => br)
                                 .ToList();

            var totalShoes = shoesQuery.Count();

            query.Brands = shoesBrands;
            query.Shoes = shoes;
            query.TotalShoes = totalShoes;

            return View(query); 
            
        }

        [HttpPost]
        public IActionResult Add(AddShoeFormModel shoe)
        {

            if (!this.data.Categories.Any(c => c.Id == shoe.CategoryId))
            {
                this.ModelState.AddModelError(nameof(shoe.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                shoe.Categories = this.GetShoeCategories();

                return View(shoe);
            }

            var shoeData = new Shoe
            {
                Brand = shoe.Brand,
                Model = shoe.Model,
                Size = shoe.Size,
                Color = shoe.Color,
                Matter = shoe.Matter,
                Description = shoe.Description,
                ImageUrl = shoe.ImageUrl,
                CategoryId = shoe.CategoryId
            };

            this.data.Shoes.Add(shoeData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
           
        }

        private IEnumerable<ShoeCategoryViewModel> GetShoeCategories()
        
            => this.data
                .Categories
                .Select(s => new ShoeCategoryViewModel
                {
                    Id = s.Id,
                    Name = s.Name

                })
                .ToList();

        
    }
}
