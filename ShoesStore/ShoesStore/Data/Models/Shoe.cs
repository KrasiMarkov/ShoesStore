using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ShoesStore.Data.DataConstants;

namespace ShoesStore.Data.Models
{

    public class Shoe
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(ShoeBrandMaxLength)]
        public string Brand { get; set; }
        [Required]
        [MaxLength(ShoeModelMaxLength)]
        public string Model { get; set; }

        [Required]
        [Range(ShoeSizeMinValue ,ShoeSizeMaxValue)]
        public int Size { get; set; }

        [Required]
        [MaxLength(ShoeColorMaxLength)]
        public string Color { get; set; }

        [Required]
        [MaxLength(ShoeMatterMaxLength)]
        public string Matter { get; set; }

        [Required]
        public string  Description { get; set; }

        [Required]
        public string  ImageUrl { get; set; }


        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
