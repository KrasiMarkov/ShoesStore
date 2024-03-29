﻿using AutoMapper;
using ShoesStore.Data.Models;

using ShoesStore.Models.Shoes;
using ShoesStore.Services.Shoes;
using ShoesStore.Services.Shoes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ShoesStore.Data.Infrastructure
{
    public class MappingProfile : Profile 
    {

        public MappingProfile()
        {
            this.CreateMap<Category, ShoeCategoryServiceModel>();
            this.CreateMap<ShoeDetailsServiceModel, ShoeFormModel>();
            this.CreateMap<Shoe, LatestShoeServiceModel>();
            this.CreateMap<Shoe, ShoeDetailsServiceModel>()
                .ForMember(s => s.UserId, cfg => cfg.MapFrom(s => s.Seller.UserId))
                .ForMember(s => s.CategoryName, cfg => cfg.MapFrom(s => s.Category.Name));
            this.CreateMap<Shoe, ShoeServiceModel>()
                .ForMember(s => s.CategoryName, cfg => cfg.MapFrom(s => s.Category.Name));
        }
    }
}
