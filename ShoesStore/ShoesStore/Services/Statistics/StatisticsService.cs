using ShoesStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ShoesStoreDbContext data;


        public StatisticsService(ShoesStoreDbContext data) 
            => this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalShoes = this.data.Shoes.Count(s => s.IsPublic);

            var totalUsers = this.data.Users.Count();


            return new StatisticsServiceModel
            {
                TotalShoes = totalShoes,
                TotalUsers = totalUsers
            };
        }
    }
}
