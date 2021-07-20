using Microsoft.AspNetCore.Mvc;
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

        public IActionResult All()
        {
            var shoes = this.data
                            .Shoes
                            .OrderByDescending(s => s.Id)
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

            return View(shoes);

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
