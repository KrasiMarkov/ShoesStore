using ShoesStore.Services.Shoes.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ShoesStore.Data.DataConstants.Shoe;

namespace ShoesStore.Models.Shoes
{
    public class ShoeFormModel : IShoeModel
    {
        [Required]
        [StringLength(BrandMaxLength, MinimumLength = BrandMinLength)]
        public string Brand { get; init; }

        [Required]
        [StringLength(ModelMaxLength, MinimumLength = ModelMinLength)]
        public string Model { get; init; }

        [Required]
        [Range(SizeMinValue, SizeMaxValue)]
        public int Size { get; init; }

        [Required]
        [StringLength(ColorMaxLength, MinimumLength = ColorMinLength)]
        public string Color { get; init; }

        [Required]
        [StringLength(MatterMaxLength, MinimumLength = MatterMinLength)]
        public string Matter { get; init; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; init; }

        [Display(Name = "Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        [Required]
        [Range(PriceMinValue, PriceMaxValue)]
        public decimal Price { get; init; }

        public IEnumerable<ShoeCategoryServiceModel> Categories { get; set; }

       

    }
}
