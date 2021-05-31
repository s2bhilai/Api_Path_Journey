using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.Api.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int NumberOfPointsOfInterest
        {
            get
            {
                return PointOfInterest.Count;
            }
        }

        public ICollection<PointOfInterestDto> PointOfInterest { get; set; }
             = new List<PointOfInterestDto>();

    }
}
