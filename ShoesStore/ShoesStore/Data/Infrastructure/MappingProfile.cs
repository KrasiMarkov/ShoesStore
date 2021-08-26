using AutoMapper;
using ShoesStore.Data.Models;
using ShoesStore.Models.Home;
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
            this.CreateMap<ShoeDetailsServiceModel, ShoeFormModel>();
            this.CreateMap<Shoe, LatestShoeServiceModel>();
            this.CreateMap<Shoe, ShoeDetailsServiceModel>()
                .ForMember(s => s.UserId, cfg => cfg.MapFrom(s => s.Seller.UserId));

        }
    }
}
