using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Services.Shoes.Models
{
    public interface IShoeModel
    {
        string Brand { get; }

        string Model { get; }

        int Size { get; }

    }
}
