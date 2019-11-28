using HerbShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Linq;

namespace HerbShop.Services
{
    public class HerbsDbContext : DbContext
    {
        public HerbsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<Herb> Herbs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public IQueryable<Item> Items => from products in Products
                                    join herbs in Herbs on products.HerbId equals herbs.Id
                                    select new Item
                                    {
                                        Id = products.Id,
                                        HerbId = herbs.Id,
                                        Name = herbs.Name,
                                        Price = products.Price,
                                        Quantity = products.Quantity,
                                        Type = herbs.Type,
                                        Unit = products.Unit,
                                        UnitSymbol = products.UnitSymbol
                                    };

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=herbshop.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Error>().ToTable("Errors");
            modelBuilder.Entity<Herb>().ToTable("Herbs");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
