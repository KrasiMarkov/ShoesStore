using Moq;
using ShoesStore.Services.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesStore.Tests.Moqs
{
    public static class StatisticsServiceMock
    {

        public static IStatisticsService Instance
        {
            get 
            {
                var statisticsServiceMock = new Mock<IStatisticsService>();

                statisticsServiceMock
                    .Setup(s => s.Total())
                    .Returns(new StatisticsServiceModel
                    {
                        TotalShoes = 5,
                        TotalSales = 10,
                        TotalUsers = 4
                    });

                return statisticsServiceMock.Object;
            }
        }

        
    }
}
