﻿
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoesStore.Data.Models;


namespace ShoesStore.Data
{
    public class ShoesStoreDbContext : IdentityDbContext<User>
    { 

        public ShoesStoreDbContext(DbContextOptions<ShoesStoreDbContext> options)
            : base(options)
        {

        }


        public DbSet<Shoe> Shoes { get; init; }

        public DbSet<Category> Categories { get; init; }


        public DbSet<Seller> Sellers { get; init; }


        public DbSet<Cart> Carts { get; set; }


        public DbSet<Order> Orders { get; set; }


        public DbSet<OrderDetail> OrderDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Shoe>()
                .HasOne(s => s.Category)
                .WithMany(s => s.Shoes)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Shoe>()
                .HasOne(sh => sh.Seller)
                .WithMany(sl => sl.Shoes)
                .HasForeignKey(sh => sh.SellerId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Seller>()
                 .HasOne<User>()
                 .WithOne()
                 .HasForeignKey<Seller>(s => s.UserId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(k => k.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            

            base.OnModelCreating(builder);
            
        }
    }
}
