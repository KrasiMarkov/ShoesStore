using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoesStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Data.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopeServices = app.ApplicationServices.CreateScope();

            var data = scopeServices.ServiceProvider.GetService<ShoesStoreDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

        private static void SeedCategories(ShoesStoreDbContext data)
        {
            if (data.Categories.Any())
            {
                return;

            }

            data.Categories.AddRange(new[]
            {
                new Category{Name = "Mens"},
                new Category{Name = "Ladies"},
                new Category{Name = "Kids" }

            });

            data.SaveChanges();
        }
    }
}
