using Microsoft.EntityFrameworkCore;
using ShoesStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesStore.Tests.Mock
{
    public static class DatabaseMock
    {
        public static ShoesStoreDbContext Instance
        {
            get 
            {
                var dbContextOptions = new DbContextOptionsBuilder<ShoesStoreDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new ShoesStoreDbContext(dbContextOptions);


            }

        }

    }
}
