using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }

        //public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "The one that got big spark",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "PoI1",
                            Description = "test desc"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "PoI2",
                            Description = "test desc"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Antwerp",
                    Description = "The one that got big spark",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 3,
                            Name = "PoI3",
                            Description = "test desc"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 4,
                            Name = "PoI4",
                            Description = "test desc"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Paris",
                    Description = "The one that got big spark",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 5,
                            Name = "PoI3",
                            Description = "test desc"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 6,
                            Name = "PoI4",
                            Description = "test desc"
                        }
                    }
                }
            };
        }
    }
}
