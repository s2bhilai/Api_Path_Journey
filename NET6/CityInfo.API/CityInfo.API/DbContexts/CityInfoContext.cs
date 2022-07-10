using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.API.DbContexts
{
    //Multiple contexts can work on the same database
    public class CityInfoContext: DbContext
    {
        public DbSet<City> Cities { get; set; } = null!;

        public DbSet<PointOfInterest> PointOfInterest { get; set; } = null!;

        public CityInfoContext(DbContextOptions<CityInfoContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City("New York City")
                {
                    Id = 1,
                    Description = "The one that got big spark",                 
                },
                new City("Antwerp")
                {
                    Id = 2,
                    Description = "The one that got big spark",                 
                },
                new City("Paris")
                {
                    Id = 3,
                    Description = "The one that got big spark",                   
                });


            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                  new PointOfInterest("Central Park")
                  {
                      Id = 1,
                      CityId = 1,
                      Description = "test desc"
                  },
                  new PointOfInterest("Empire State Building")
                  {
                      Id = 2,
                      CityId = 1,
                      Description = "test desc"
                  },
                   new PointOfInterest("Cathedral")
                   {
                       Id = 3,
                       CityId = 2,
                       Description = "test desc"
                   }
                );
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
