
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
        //[Fact]
        //public void IndexShouldReturnViewWithCorrectModelAndData()
        //{
        //    //Arrange

        //    var controller = MyController<HomeController>
        //       .Instance(controller => controller
        //       .WithData(TenPublicShoes()));

        //    //Act

        //    var actionCall = controller.Calling(s => s.Index());

        //    //Assert

        //    actionCall
        //        .ShouldReturn()
        //        .View(view => view.WithModelOfType<List<LatestShoeServiceModel>>()
        //        .Passing(m => m.Should().HaveCount(3)));

        //}

        //[Fact]
        //public void IndexShouldReturnViewWithCorrectModel()
        //{
        //    //Arrange 
        //    var data = DatabaseMock.Instance;
        //    var mapper = MapperMock.Instance;

        //    data.Shoes.AddRange(Enumerable.Range(0, 10).Select(i => new Shoe()));
        //    data.Users.Add(new User());
        //    data.SaveChanges();

        //    var shoeService = new ShoeService(data, mapper);
        //    var statisticsService = new StatisticsService(data);

        //    var homeController = new HomeController(shoeService);

        //    //Act

        //    var result = homeController.Index();

        //    //Assert

        //    Assert.NotNull(result);

        //    var viewResult = Assert.IsType<ViewResult>(result);

        //    var model = viewResult.Model;

        //    var indexViewModel = Assert.IsType<List<LatestShoeServiceModel>>(model);

        //    Assert.Equal(3, indexViewModel.);
        //    Assert.Equal(10, indexViewModel.TotalShoes);
        //    Assert.Equal(1, indexViewModel.TotalUsers);
        //}


        //[Fact]
        //public void ErrorShouldReturnView()
        //{
        //    //Arrange

        //    var homeController = new HomeController(null, null);

        //    //Act

        //    var result = homeController.Error();

        //    //Assert

        //    Assert.NotNull(result);
        //    Assert.IsType<ViewResult>(result);
        //}

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
