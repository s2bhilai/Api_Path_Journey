using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
//Route - Doesn't map to Http Methods
// Use it at controller level to provide a template that
// will prefix all templates defined at action level

//Dont send back 200 OK when omething's wrong
//Dont send back 500 Internal Server Error when client makes mistake.

//Content Negotiation: Process of selecting the best representation
//for a given response when there are multiple representations available.

//Output Formatter: Deals with Output : Media Type: Accept header
// Input Formatter: Deals with Input: Media Type: Content-Type header
// Support is implemented by Object Result - Action result methods derive from it

//Binding source attributes tell the model binding engine where to find the binding source.
// FromBody,FromForm,FromHeader,FromQuery,FromRoute,FromService

// By default Asp Net Core attempts to use the complex object model binder.
// The [ApiController] changes the rules to better fit Apis
// FromBody - inferred for complex types.
//FromForm - Inferred for action parameters of type IFormFile and IFormFileCollection.
//FromRoute - Inferred for any action parameter name matching  a parameter in route template
//FromQuery - Inferred for any other action parameters

//If controller annotated with [ApiController] it will automatically return 400
// on validation errors for ex: empty body on post

//Tight Coupling
//Class implementation has to change when a dependency changes
//Difficult to test
//Class manages the lifetime of dependency

//Inversion of Control 
//IoC delegates the function of selecting a concrete implementation type
//for a class's dependencies to an external component

//Dependency Injection
//A specialization of IoC pattern which uses an object-the container-
//to initialize objects and provide the required dependecies to the object.
//Services are registered on the container
//The container becomes responsible for providing instances when needed. It
//manages the service lifetime

//An Open API specification describes the capabilities of your API
//and how to interact with it.
//It is standardized and in JSON or YAML format
//Open API specification and Swagger specification are same thing
//Swagger is a set of open source tools built around that open API specification 
//Swashbuckle.aspnetcore helps with working with openAPI in Asp Net Core
// - Generates an Open API specification from your API.
// - Wraps swagger-ui and provides an embedded version of it.

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/cities")]
    [Authorize]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class CitiesController:ControllerBase
    {
        private ICityInfoRepository _cityInfoRepository;
        private IMapper _mapper;
        const int maxCitiesPageSize = 20;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> 
            GetCities(string? name,string? searchQuery,int pageNumber=1,int pageSize=10)
        {
            if (pageSize > maxCitiesPageSize)
                pageSize = maxCitiesPageSize;

            var (cityEntities,paginationMetadata) = await _cityInfoRepository
                .GetCitiesAsync(name,searchQuery,pageNumber,pageSize);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities));
        }


        /// <summary>
        /// Get a City by Id
        /// </summary>
        /// <param name="id">The Id of city to get</param>
        /// <param name="includePointOfInterest">Whether or not to include Point Of Interest</param>
        /// <returns>An IActionResult</returns>
        /// <response code="200">Returns the requested city</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCity(int id,bool includePointOfInterest = false)
        {
            var city = await _cityInfoRepository.GetCityAsync(id, includePointOfInterest);

            if (city == null)
                return NotFound();

            if (includePointOfInterest)
                return Ok(_mapper.Map<Models.CityDto>(city));

            return Ok(_mapper.Map<CityWithoutPointsOfInterestDto>(city));
        }
    }
}
