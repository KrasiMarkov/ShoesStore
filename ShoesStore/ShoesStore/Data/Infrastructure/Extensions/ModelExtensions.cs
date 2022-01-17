using ShoesStore.Services.Shoes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Data.Infrastructure.Extensions
{
    public static class ModelExtensions
    {

        public static string GetInformation(this IShoeModel shoe)
            => shoe.Brand + "-" + shoe.Model + "-" + shoe.Size;
    }
}
