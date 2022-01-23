using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MyTested.AspNetCore.Mvc;
using ShoesStore.Controllers;
using ShoesStore.Models.Sellers;

namespace ShoesStore.Tests.Routing
{
    public class SellersControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeMapped()
            => MyRouting
                  .Configuration()
                  .ShouldMap("/Sellers/Become")
                  .To<SellersController>(c => c.Become());


        [Fact]

        public void PostBecomeShouldBeMapped()
            => MyRouting
                  .Configuration()
                  .ShouldMap(request => request
                       .WithPath("/Sellers/Become")
                       .WithMethod(HttpMethod.Post))
                  .To<SellersController>(c => c.Become(With.Any<BecomeSellerFormModel>()));

    }
}
