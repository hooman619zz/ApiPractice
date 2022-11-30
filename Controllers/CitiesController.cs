using FirstApiProject.Models;
using FirstApiProject.UW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstApiProject.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly IUnitOfWork uw;
        public CitiesController(IUnitOfWork uw)
        {
            this.uw = uw;
        }
        [HttpGet("getAllCities")]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCities()
        {
            return Ok(await uw.cityServices.GetAllAsync());
        }

        [HttpGet("GetCityById/{id:int}")]
        public async Task<ActionResult<CityDto>> GetCity(int id)
        {
            var result = uw.cityServices.GetByIdAsync(id);

            if(result == null)
                return NotFound();

            return Ok(result);



        }
    }
}
