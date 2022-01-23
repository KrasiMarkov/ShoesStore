using ShoesStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesStore.Tests.Data
{
    public static class Shoes
    {
        public static IEnumerable<Shoe> TenPublicShoes
            => Enumerable.Range(0, 10).Select(i => new Shoe
            {
                IsPublic = true
            });

    }
}
