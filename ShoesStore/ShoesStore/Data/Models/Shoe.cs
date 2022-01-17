using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ShoesStore.Data.DataConstants.Shoe;

namespace ShoesStore.Data.Models
{

    public class Shoe
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(BrandMaxLength)]
        public string Brand { get; set; }
        [Required]
        [MaxLength(ModelMaxLength)]
        public string Model { get; set; }

        [Required]
        [Range(SizeMinValue ,SizeMaxValue)]
        public int Size { get; set; }

        [Required]
        [MaxLength(ColorMaxLength)]
        public string Color { get; set; }

        [Required]
        [MaxLength(MatterMaxLength)]
        public string Matter { get; set; }

        [Required]
        public string  Description { get; set; }

        [Required]
        public string  ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public bool IsPublic { get; set; }

        public Category Category { get; set; }

        public int SellerId { get; init; }

        public Seller Seller { get; init; }
    }
}
