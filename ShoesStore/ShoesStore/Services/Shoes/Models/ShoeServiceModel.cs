using ShoesStore.Services.Shoes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Services.Shoes
{
    public class ShoeServiceModel : IShoeModel
    {
        public int Id { get; init; }

        public string Brand { get; init; }

        public string Model { get; init; }

        public int Size { get; init; }

        public string Color { get; init; }

        public string Matter { get; init; }

        public string ImageUrl { get; init; }

        public string CategoryName { get; init; }


        public bool IsPublic { get; init; }

    }
}
