using CityInfo.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.Api
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public List<CityDto> Cities { get; set; }
        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id=1,
                    Name="Bhilai",
                    Description="Bhilai",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "poi1",
                            Description = "doi1"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "poi2",
                            Description = "doi2"
                        }
                    }
                },
                new CityDto()
                {
                    Id=2,
                    Name="Chengannur",
                    Description="Chengannur",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 3,
                            Name = "poi3",
                            Description = "doi3"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 4,
                            Name = "poi4",
                            Description = "doi4"
                        }
                    }
                },
                new CityDto()
                {
                    Id=3,
                    Name="Doha",
                    Description="Doha",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 5,
                            Name = "poi5",
                            Description = "doi5"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 6,
                            Name = "poi6",
                            Description = "doi6"
                        }
                    }
                }
            };
        }
    }
}
