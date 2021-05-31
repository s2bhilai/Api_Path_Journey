using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.Api.Controllers
{
    //[Route("api/cities")]
    //or
    [ApiController]
    [Route("api/cities")]
    public class CitiesController: ControllerBase
    {
        
        [HttpGet]
        public IActionResult GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            //find city
            var city = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == id);

            if (city == null) return NotFound();

            return Ok(city);
                
        }
    }
}
