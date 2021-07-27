using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesStore.Data;
using ShoesStore.Data.Infrastructure;
using ShoesStore.Data.Models;
using ShoesStore.Models.Shoes;
using ShoesStore.Services.Shoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Controllers
{
    public class ShoesController : Controller
    {
        private readonly ShoesStoreDbContext data;
        private readonly IShoeService shoes;

        public ShoesController(ShoesStoreDbContext data, IShoeService shoes)
        {
            this.data = data;
            this.shoes = shoes;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsSeller())
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            return View(new AddShoeFormModel
            {
                Categories = this.GetShoeCategories()

            });
        }

        public IActionResult All([FromQuery] AllShoesQueryModel query)
        {
            var queryResult = this.shoes
                            .All(query.Brand,
                                 query.SearchTerm,
                                 query.Sorting,
                                 query.CurrentPage,
                                 AllShoesQueryModel.ShoesPerPage);

            var shoesBrands = this.shoes.AllShoeBrands();

            

            query.Brands = shoesBrands;
            query.TotalShoes = queryResult.TotalShoes;
            query.Shoes = queryResult.Shoes;

            return View(query); 
            
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddShoeFormModel shoe)
        {
            

            var sellerId = this.data
                               .Sellers
                               .Where(s => s.UserId == this.User.GetId())
                               .Select(s => s.Id)
                               .FirstOrDefault();


            if (sellerId == 0)
            {
                return RedirectToAction(nameof(SellersController.Become), "SellersController");
            }


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

        public IActionResult Mine()
        {
            var myShoes = this.shoes.ByUsers(this.User.GetId());

            return View(myShoes);
        }

        private bool UserIsSeller()
            => this.data
                .Sellers
                .Any(s => s.UserId == this.User.GetId());

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
