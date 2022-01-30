
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoesStore.Controllers;
using Moq;
using Xunit;
using ShoesStore.Services.Shoes;
using ShoesStore.Tests;
using System.Collections.Generic;
using ShoesStore.Services.Statistics;

using System.Linq;
using ShoesStore.Data.Models;
using MyTested.AspNetCore.Mvc;
using FluentAssertions;
using ShoesStore.Services.Shoes.Models;
using static ShoesStore.Tests.Data.Shoes;

namespace ShoesStore.Tests.Pipeline
{
    public class  HomeControllerTest
    {

        [Fact]
        public void GetIndexShouldReturnViewWithCorrectModelAndData()
            => MyMvc
                .Pipeline()
                .ShouldMap("/")
                .To<HomeController>(s => s.Index())
                .Which(controller => controller
                    .WithData(TenPublicShoes))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<LatestShoeServiceModel>>()
                    .Passing(m => m.Should().HaveCount(3)))
            .AndAlso();

        [Fact]
        public void GetErrorShouldReturnView()
            => MyMvc
                .Pipeline()
                .ShouldMap("/Home/Error")
                .To<HomeController>(s => s.Error())
                .Which()
                .ShouldReturn()
                .View();
    }
}
