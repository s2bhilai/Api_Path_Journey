using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [Route("api/v{version:apiVersion}/cities/{cityId}/pointsofinterest")]
    [ApiController]
    [ApiVersion("2.0")]
    [Authorize(Policy = "MustBeFromParis")]
    public class PointsOfInterestController:ControllerBase
    {
        private ILogger<PointsOfInterestController> _logger;
        private IMailService _mailService;
        private ICityInfoRepository _cityInfoRepository;
        private IMapper _mapper;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            IMailService mailService, ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));      
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> 
            GetPointsOfInterest(int cityId)
        {
            var cityName = User.Claims.FirstOrDefault(c => c.Type == "city")?.Value;

            if(!await _cityInfoRepository.CityNameMatchesCityId(cityName,cityId))
            {
                return Forbid();
            }

            if(!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"City with Id {cityId} wasn't found when accessing" +
                        $"points of interest");
                return NotFound();
            }

            var pointOfInterestForCity = await _cityInfoRepository
                .GetPointsOfInterestForCityAsync(cityId);

            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointOfInterestForCity));
        }

        [HttpGet("{pointOfInterestId}",Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(
            int cityId, int pointOfInterestId)
        {
            try
            {
                if (!await _cityInfoRepository.CityExistsAsync(cityId))
                {
                    _logger.LogInformation($"City with Id {cityId} wasn't found when accessing" +
                            $"points of interest");
                    return NotFound();
                }

                var pointOfInterest = await _cityInfoRepository
                    .GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

                if (pointOfInterest == null)
                    return NotFound();

                return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while getting pointofinterest for city" +
                    $"with id {cityId}. and pointofinterest: {pointOfInterestId}", ex);

                return StatusCode(500, "A problem happened while handling your request");
            }           
        }

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(
            int cityId,PointOfInterestForCreationDto pointOfInterest)
        {
            if(!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var finalPointOfInterest = _mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            await _cityInfoRepository.AddPointOfInterestForCityAsync(
                cityId, finalPointOfInterest);

            await _cityInfoRepository.SaveChangesAsync();

            var createdPointOfInterestToReturn = 
                _mapper.Map<Models.PointOfInterestDto>(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",
               new
               {
                   cityId = cityId,
                   pointOfInterestId = createdPointOfInterestToReturn.Id
               }, createdPointOfInterestToReturn);



            //var city = _citiesDataStore
            //    .Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null)
            //    return NotFound();

            //IEnumerable<ICollection<PointOfInterestDto>> query
            //    = CitiesDataStore.Current.Cities.Select(s => s.PointsOfInterest);

            //foreach (List<PointOfInterestDto> item in query)
            //{

            //}

            //Instead of above 2 foreach's , Use SelectMany

            //var maxPointOfInterestId = _citiesDataStore.Cities
            //    .SelectMany(c => c.PointsOfInterest).Max(p => p.Id);

            //var finalPointOfInterest = new PointOfInterestDto()
            //{
            //    Id = ++maxPointOfInterestId,
            //    Name = pointOfInterest.Name,
            //    Description = pointOfInterest.Description
            //};

            //city.PointsOfInterest.Add(finalPointOfInterest);

           
        }

        [HttpPut("{pointofinterestid}")]
        public async Task<ActionResult> UpdatePointOfInterest(int cityId,int pointofInterestId,
            PointOfInterestForUpdateDto pointOfInterest)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = await _cityInfoRepository
                  .GetPointOfInterestForCityAsync(cityId, pointofInterestId);

            if (pointOfInterestEntity == null)
                return NotFound();

            _mapper.Map(pointOfInterest, pointOfInterestEntity);

            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();

            //var city = _citiesDataStore.Cities
            //    .FirstOrDefault(c => c.Id == cityId);

            //if (city == null)
            //    return NotFound();

            ////find point of interest
            //var pointOfInterestFromStore = city.PointsOfInterest
            //    .FirstOrDefault(c => c.Id == pointofInterestId);
            //if (pointOfInterestFromStore == null)
            //    return NotFound();

            //pointOfInterestFromStore.Name = pointOfInterest.Name;
            //pointOfInterestFromStore.Description = pointOfInterest.Description;

            //return NoContent();
        }

        [HttpPatch("{pointOfInterestId}")]
        public async Task<ActionResult> PartiallyUpdatePointOfInterest(
            int cityId,int pointOfInterestId,
            JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = await _cityInfoRepository
                .GetPointOfInterestForCityAsync(cityId, pointOfInterestId);
            if (pointOfInterestEntity == null)
                return NotFound();

            var pointOfInterestToPatch =
                _mapper.Map<PointOfInterestForUpdateDto>(pointOfInterestEntity);

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);

            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();

            //var city = _citiesDataStore.Cities
            //    .FirstOrDefault(c => c.Id == cityId);

            //if (city == null)
            //    return NotFound();

            //var pointOfInterestFromStore = city.PointsOfInterest
            //    .FirstOrDefault(c => c.Id == pointOfInterestId);
            //if (pointOfInterestFromStore == null)
            //    return NotFound();

            //var pointOfInterestToPatch =
            //    new PointOfInterestForUpdateDto()
            //    {
            //        Name = pointOfInterestFromStore.Name,
            //        Description = pointOfInterestFromStore.Description
            //    };

            //patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if(!TryValidateModel(pointOfInterestToPatch))
            //{
            //    return BadRequest(ModelState);
            //}

            //pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            //pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            //return NoContent();
        }

        [HttpDelete("{pointOfInterestId}")]
        public async Task<ActionResult> DeletePointOfInterest(int cityId,int pointOfInterestId)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = await _cityInfoRepository
               .GetPointOfInterestForCityAsync(cityId, pointOfInterestId);
            if (pointOfInterestEntity == null)
                return NotFound();

            _cityInfoRepository.DeletePointOfInterest(pointOfInterestEntity);

            await _cityInfoRepository.SaveChangesAsync();

            _mailService.Send("point of interest deleted",
                $"point of interest {pointOfInterestEntity.Name} with id {pointOfInterestEntity.Id}");

            return NoContent();

            //var city = _citiesDataStore.Cities
            //    .FirstOrDefault(c => c.Id == cityId);

            //if (city == null)
            //    return NotFound();

            //var pointOfinterestFromStore = city.PointsOfInterest
            //    .FirstOrDefault(c => c.Id == pointOfInterestId);

            //if (pointOfinterestFromStore == null)
            //    return NotFound();

            //city.PointsOfInterest.Remove(pointOfinterestFromStore);
            //_mailService.Send("point of interest deleted",
            //    $"point of interest {pointOfinterestFromStore.Name} with id {pointOfinterestFromStore.Id}");


        }
    }
}
