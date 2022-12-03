using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Data.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ShoeId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public  Order Order { get; set; }

        public Shoe Shoe { get; set; }

    }
}
