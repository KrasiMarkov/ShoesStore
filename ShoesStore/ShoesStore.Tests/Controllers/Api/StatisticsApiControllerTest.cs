using ShoesStore.Controllers.Api;
using ShoesStore.Tests.Moqs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShoesStore.Tests.Controllers.Api
{
    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatisticsShouldReturnTotalStatistics()
        {
            //Arrange

            var statisticsController = new StatisticsApiController(StatisticsServiceMock.Instance);

            //Act

            var result = statisticsController.GetStatistics();

            //Assert

            Assert.NotNull(result);
            Assert.Equal(5, result.TotalShoes);
            Assert.Equal(10, result.TotalSales);
            Assert.Equal(4, result.TotalUsers);

        }
    }
}
