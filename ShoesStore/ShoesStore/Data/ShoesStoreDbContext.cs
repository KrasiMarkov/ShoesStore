using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoesStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesStore.Data
{
    public class ShoesStoreDbContext : IdentityDbContext
    { 

        public ShoesStoreDbContext(DbContextOptions<ShoesStoreDbContext> options)
            : base(options)
        {

        }


        public DbSet<Shoe> Shoes { get; init; }

        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Shoe>()
                .HasOne(s => s.Category)
                .WithMany(s => s.Shoes)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);



            base.OnModelCreating(builder);
        }
    }
}
