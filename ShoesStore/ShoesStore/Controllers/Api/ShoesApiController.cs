using Microsoft.AspNetCore.Mvc;
using ShoesStore.Models.Api.Shoes;
using ShoesStore.Services.Shoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Controllers.Api
{
    
    
        [ApiController]
        [Route("api/shoes")]
        public class ShoesApiController : ControllerBase
        {
            private readonly IShoeService shoes;

            public ShoesApiController(IShoeService shoes)
                => this.shoes = shoes;

            [HttpGet]
            public ShoeQueryServiceModel All([FromQuery] AllShoesApiRequestModel query)
                => this.shoes.All(
                    query.Brand,
                    query.SearchTerm,
                    query.Sorting,
                    query.CurrentPage,
                    query.CarsPerPage);
        }

    
}
