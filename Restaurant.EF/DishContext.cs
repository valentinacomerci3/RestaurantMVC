using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.EF
{
    public class DishContext : DbContext
    {
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public DishContext(DbContextOptions<DishContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>().ToTable("Dish")
                                           .HasKey(k => k.Id);
            modelBuilder.Entity<Dish>().Property(f => f.Name).IsRequired();
            modelBuilder.Entity<Dish>().Property(l => l.Description).IsRequired();
            modelBuilder.Entity<Dish>().Property(d => d.Price).IsRequired();
            modelBuilder.Entity<Dish>().Property(r => r.Categ).IsRequired();

            modelBuilder.Entity<Account>().ToTable("Users").HasKey(k => k.Id);
            modelBuilder.Entity<Account>().Property(u => u.Username).IsRequired();
            modelBuilder.Entity<Account>().Property(p => p.Password).IsRequired();
            modelBuilder.Entity<Account>().Property(r => r.Role).IsRequired();

            modelBuilder.Entity<Dish>().HasData(
               new Dish { Id = 1, Name = "Carbonara", Description = "Roman Staple", Price = 10, Categ = Core.Models.Categ.FirstCourse },
               new Dish { Id = 2, Name = "Gnocchi", Description = "Baked with fresh mozzarella", Price = 10, Categ = Categ.FirstCourse },

               new Dish { Id = 3, Name = "PrimeRib", Description = "GrassFed Beef with comes with side of fresh veggies", Price = 20, Categ = Categ.SecondCourse },
               new Dish { Id = 4, Name = "PorkChop", Description = "Slow cooked with zesty aromas", Price = 20, Categ = Categ.SecondCourse },

               new Dish { Id = 5, Name = "Fries", Description = "Our hand-cut fries with aioli home made sauce", Price = 7,Categ = Categ.Side },
               new Dish { Id = 6, Name = "Spring Sprouts", Description = "Seasoned with herbs from our garden", Price = 7, Categ = Categ.Side },

               new Dish { Id = 7, Name = "Tiramisu", Description = "Nonna made it, just saying", Price = 8, Categ = Categ.Dessert },
               new Dish { Id = 8, Name = "Panna Cotta", Description = "Wobbly delicacy with a dark chocolate topping", Price = 8, Categ = Categ.Dessert }
               
           );

            modelBuilder.Entity<Account>().HasData(
                new Account { Id = 1, Username = "paoloson3@gmee.com", Password = "0000", Role = Role.Manager },
                new Account { Id = 2, Username = "user123@abc.it", Password = "2222", Role = Role.Client }
            );

        }
    }
}
