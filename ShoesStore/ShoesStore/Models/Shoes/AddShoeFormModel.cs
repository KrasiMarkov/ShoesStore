using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Models.Shoes
{
    public class AddShoeFormModel
    {
       
        public string Brand { get; init; }
      
        public string Model { get; init; }

       
        public int Size { get; init; }

       
        public string Color { get; init; }

       
        public string Matter { get; init; }

        
        public string Description { get; init; }

        
        public string ImageUrl { get; init; }


        public int CategoryId { get; init; }

       

    }
}
