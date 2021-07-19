using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ShoesStore.Data.DataConstants;

namespace ShoesStore.Models.Shoes
{
    public class AddShoeFormModel
    {
        [Required]
        [StringLength(ShoeBrandMaxLength, MinimumLength = ShoeBrandMinLength)]
        public string Brand { get; init; }

        [Required]
        [StringLength(ShoeModelMaxLength, MinimumLength = ShoeModelMinLength)]
        public string Model { get; init; }

        [Required]
        [Range(ShoeSizeMinValue, ShoeSizeMaxValue)]
        public int Size { get; init; }

        [Required]
        [StringLength(ShoeColorMaxLength, MinimumLength = ShoeColorMinLength)]
        public string Color { get; init; }

        [Required]
        [StringLength(ShoeMatterMaxLength, MinimumLength = ShoeMatterMinLength)]
        public string Matter { get; init; }

        [Required]
        [StringLength(ShoeDescriptionMaxLength, MinimumLength = ShoeDescriptionMinLength)]
        public string Description { get; init; }

        [Display(Name = "Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<ShoeCategoryViewModel> Categories { get; set; }

       

    }
}
