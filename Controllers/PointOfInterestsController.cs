using FirstApiProject;
using FirstApiProject.Models;
using FirstApiProject.Services;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    //  /api/cities/3/pointsofinterest
    [Route("api/cities/{cityId:int}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        #region Ctor & DI
        private IValidator<PointOfInterestForCreationDto> _validatorForCreation;
        private IValidator<PointOfInterestForUpdateDto> _validatorForUpdate;
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService   localMailService;
        private readonly CitiesDataStore citiesDataStore;

        public PointsOfInterestController(
            IValidator<PointOfInterestForCreationDto> validatorCreation
            , IValidator<PointOfInterestForUpdateDto> validatorUpdate
            , ILogger<PointsOfInterestController> logger
            ,IMailService localMailService
            ,CitiesDataStore citiesDataStore
            )
        {
            _validatorForCreation = validatorCreation;
            _validatorForUpdate = validatorUpdate;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.localMailService = localMailService ?? throw new ArgumentNullException(nameof(localMailService));
            this.citiesDataStore = citiesDataStore;
        }
        #endregion

        #region GetPoints Of a City with cityid
        [HttpGet("getPointsWithCityid")]
        public ActionResult<IEnumerable<PointOfInterestDto>>
            GetPointsOfInterest(int cityId)
        {
            var city =
                citiesDataStore.Cities
                .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                localMailService.Send("input value", $"city with this id : {cityId} cannot find !");
                return NotFound();
            }

            return Ok(city.PointOfInterests);
        }
        #endregion

        #region GetPoint with point id Get
        [HttpGet("getpointofinterest/{pointOfInterestId:int}", Name = "GetPointOfInterest")]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(
            int cityId, int pointOfInterestId
            )
        {
            try
            {
                var city =
                            citiesDataStore.Cities
                            .FirstOrDefault(c => c.Id == cityId);

                if (city == null)
                {
                    //_logger.LogInformation($"city with {cityId} doesnt find");baramoon ro console log mindaze !
                    return NotFound();
                }
                var point = city.PointOfInterests
                           .FirstOrDefault(p => p.Id == pointOfInterestId);

                if (point == null)
                {
                    return NotFound();
                }
                return Ok(point);

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"excepction",ex);
                return StatusCode(500, "problem");
                    throw;
            }



        }
        #endregion

        #region Add Point To City Post
        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(
            int cityId,
            PointOfInterestForCreationDto pointOfInterest
            )
        {
            var result = _validatorForCreation.Validate(pointOfInterest);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            var city = citiesDataStore
                .Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var maxpointOfInterestId = citiesDataStore.Cities
                .SelectMany(c => c.PointOfInterests)
                .Max(p => p.Id);

            var createPoint = new PointOfInterestDto()
            {
                Id = ++maxpointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointOfInterests.Add(createPoint);



            return CreatedAtAction("GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    pointOfInterestId = createPoint.Id

                },
                createPoint
                );
        }
        #endregion

        #region Edit Point With Put
        [HttpPut("editpointofinterest/{pointId:int}")]
        public async Task<ActionResult<PointOfInterestDto>> UpdatePointOfInterestAsync(int cityId, int pointId, PointOfInterestForUpdateDto pointOfInterest)
        {
            var city = citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var result = await _validatorForUpdate.ValidateAsync(pointOfInterest);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            var point = city.PointOfInterests.FirstOrDefault(p => p.Id == pointId);
            if (point == null)
            {
                return NotFound();
            }
            point.Name = pointOfInterest.Name;
            point.Description = pointOfInterest.Description;

            return NoContent();
        }
        #endregion

        #region Edit Point With Patch
        [HttpPatch("ParttiallyUpdatePointOfInterest/{pointId:int}")]
        public async Task<ActionResult<PointOfInterestDto>> ParttiallyUpdatePointOfInterest(
            int cityId, int pointId, JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument
            )
        {
            var city = citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();
            var pointFromDb = city.PointOfInterests.FirstOrDefault(p => p.Id == pointId);
            if (pointFromDb == null)
                return NotFound();
            var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
            {
                Name = pointFromDb.Name,
                Description = pointFromDb.Description
            };
            patchDocument.ApplyTo(pointOfInterestToPatch);
            var validation = await _validatorForUpdate.ValidateAsync(pointOfInterestToPatch);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);
            pointFromDb.Name = pointOfInterestToPatch.Name;
            pointFromDb.Description = pointOfInterestToPatch.Description;
            return NoContent();
        }
        #endregion

        #region Delete Point
        [HttpDelete("{pointId:int}/deletepoint")]
        public ActionResult DeletePoint(int cityId, int pointId)
        {
            var city = citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();
            var point = city.PointOfInterests.FirstOrDefault(p => p.Id == pointId);
            if (point == null)
                return NotFound();
            city.PointOfInterests.Remove(point);
            return NoContent();
        }
        #endregion

    }
}

