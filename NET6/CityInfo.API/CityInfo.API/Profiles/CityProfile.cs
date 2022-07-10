using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.API.Profiles
{

    //Convention based, Automapper will map the property names from source object
    //to the same property names on the destination object and by default
    // will ignore nullreferenceexceptions from source to target so if the property doesn't exist
    // it will be ignored
    public class CityProfile: Profile
    {
        public CityProfile()
        {
            CreateMap<Entities.City, Models.CityWithoutPointsOfInterestDto>();
            CreateMap<Entities.City, Models.CityDto>();
        }
    }
}
