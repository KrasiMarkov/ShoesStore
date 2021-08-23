using AutoMapper;
using ShoesStore.Data.Models;
using ShoesStore.Models.Home;
using ShoesStore.Models.Shoes;
using ShoesStore.Services.Shoes;
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
            this.CreateMap<ShoeDetailsServiceModel, ShoeFormModel>();
            this.CreateMap<Shoe, ShoeIndexViewModel>();
            this.CreateMap<Shoe, ShoeDetailsServiceModel>()
                .ForMember(s => s.UserId, cfg => cfg.MapFrom(s => s.Seller.UserId));

        }
    }
}
