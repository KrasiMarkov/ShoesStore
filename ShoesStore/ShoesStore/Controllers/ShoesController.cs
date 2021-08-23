using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesStore.Data;
using ShoesStore.Data.Infrastructure;
using ShoesStore.Data.Models;
using ShoesStore.Models.Shoes;
using ShoesStore.Services.Sellers;
using ShoesStore.Services.Shoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Controllers
{
    public class ShoesController : Controller
    {
        
        private readonly ISellerService sellers;
        private readonly IShoeService shoes;
        private readonly IMapper mapper;

        public ShoesController(IShoeService shoes, ISellerService sellers, IMapper mapper)
        {
            this.shoes = shoes;
            this.sellers = sellers;
            this.mapper = mapper;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.sellers.IsSeller(this.User.Id()))
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            return View(new ShoeFormModel
            {
                Categories = this.shoes.AllCategories()

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

            var shoesBrands = this.shoes.AllBrands();



            query.Brands = shoesBrands;
            query.TotalShoes = queryResult.TotalShoes;
            query.Shoes = queryResult.Shoes;

            return View(query);

        }


        [HttpPost]
        [Authorize]
        public IActionResult Add(ShoeFormModel shoe)
        {


            var sellerId = this.sellers.IdByUser(this.User.Id());


            if (sellerId == 0)
            {
                return RedirectToAction(nameof(SellersController.Become), "SellersController");
            }


            if (!this.shoes.CategoryExists(shoe.CategoryId))
            {
                this.ModelState.AddModelError(nameof(shoe.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                shoe.Categories = this.shoes.AllCategories();

                return View(shoe);
            }


            this.shoes.Create(
                shoe.Brand,
                shoe.Model,
                shoe.Size,
                shoe.Color,
                shoe.Matter,
                shoe.Description,
                shoe.ImageUrl,
                shoe.CategoryId,
                sellerId);


            return RedirectToAction(nameof(All));

        }

        [Authorize]
        public IActionResult Mine()
        {
            var myShoes = this.shoes.ByUsers(this.User.Id());

            return View(myShoes);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.sellers.IsSeller(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            var shoe = this.shoes.Details(id);

            if (shoe.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }


            var shoeForm = this.mapper.Map<ShoeFormModel>(shoe);

            shoeForm.Categories = this.shoes.AllCategories();

            return View(shoeForm);
            

        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, ShoeFormModel shoe)
        {
            var sellerId = this.sellers.IdByUser(this.User.Id());


            if (sellerId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(SellersController.Become), "SellersController");
            }


            if (!this.shoes.CategoryExists(shoe.CategoryId))
            {
                this.ModelState.AddModelError(nameof(shoe.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                shoe.Categories = this.shoes.AllCategories();

                return View(shoe);
            }


            if (!this.shoes.IsBySeller(id, sellerId) && !User.IsAdmin())
            {
                return BadRequest();
            }


            this.shoes.Edit(
                id,
                shoe.Brand,
                shoe.Model,
                shoe.Size,
                shoe.Color,
                shoe.Matter,
                shoe.Description,
                shoe.ImageUrl,
                shoe.CategoryId);

          

            return RedirectToAction(nameof(All));


        }




    }
}
