using Microsoft.AspNetCore.Mvc;
using ShoesStore.Models.Shoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Controllers
{
    public class ShoesController : Controller
    {
        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddShoeFormModel shoe)
        {
            return View();

        }
    }
}
