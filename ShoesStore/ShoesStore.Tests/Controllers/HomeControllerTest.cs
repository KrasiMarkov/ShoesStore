using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MyTested.AspNetCore.Mvc;
using ShoesStore.Controllers;
using FluentAssertions;
using ShoesStore.Services.Shoes.Models;
using static ShoesStore.Tests.Data.Shoes;
using static ShoesStore.WebConstants.Cache;

namespace ShoesStore.Tests.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void GetIndexShouldReturnCorrectViewWithModel()
            => MyController<HomeController>
               .Instance(instance => instance
                   .WithData(TenPublicShoes))
               .Calling(c => c.Index())
               .ShouldReturn()
               .View(view => view
                    .WithModelOfType<List<LatestShoeServiceModel>>()
                    .Passing(model => model.Should().HaveCount(3)));


        [Fact]
        public void GetErrorShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();
    }
}
