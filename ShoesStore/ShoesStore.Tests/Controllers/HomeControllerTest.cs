
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoesStore.Controllers;
using Moq;
using Xunit;
using ShoesStore.Services.Shoes;
using ShoesStore.Tests.Mock;
using System.Collections.Generic;
using ShoesStore.Services.Statistics;
using ShoesStore.Models.Home;
using System.Linq;
using ShoesStore.Data.Models;
using MyTested.AspNetCore.Mvc;
using FluentAssertions;

namespace ShoesStore.Tests.Controller
{
    public class  HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
        {
            //Arrange

            var controller = MyController<HomeController>
               .Instance(controller => controller
               .WithData(GetShoes()));

            //Act

            var actionCall = controller.Calling(s => s.Index());

            //Assert

            actionCall
                .ShouldReturn()
                .View(view => view.WithModelOfType<IndexViewModel>()
                .Passing(m => m.Shoes.Should().HaveCount(3)));

        }

        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            //Arrange 
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Shoes.AddRange(Enumerable.Range(0, 10).Select(i => new Shoe()));
            data.Users.Add(new User());
            data.SaveChanges();

            var shoeService = new ShoeService(data, mapper);
            var statisticsService = new StatisticsService(data);

            var homeController = new HomeController(statisticsService, shoeService);

            //Act

            var result = homeController.Index();

            //Assert

            Assert.NotNull(result);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var indexViewModel = Assert.IsType<IndexViewModel>(model);

            Assert.Equal(3, indexViewModel.Shoes.Count);
            Assert.Equal(10, indexViewModel.TotalShoes);
            Assert.Equal(1, indexViewModel.TotalUsers);
        }


        [Fact]
        public void ErrorShouldReturnView()
        {
            //Arrange

            var homeController = new HomeController(null, null);

            //Act

            var result = homeController.Error();

            //Assert

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        private static IEnumerable<Shoe> GetShoes()
            => Enumerable.Range(0, 10).Select(i => new Shoe());



       

    }
}
