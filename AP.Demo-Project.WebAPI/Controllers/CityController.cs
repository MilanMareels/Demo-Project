using AP.Demo_Project.Application.CQRS.City;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AP.Demo_Project.WebAPI.Controllers
{
    public class CityController : APIv1Controller
    {
        private readonly IMediator mediator;

        public CityController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities([FromQuery] int pageNr = 1, [FromQuery] int pageSize = 5, [FromQuery] string sortBy = "Population", [FromQuery] string sortOrder = "asc")
        {
            return Ok(await mediator.Send(new GetAllCitiesQuery() { PageNr = pageNr, PageSize = pageSize, SortBy = sortBy, SortOrder = sortOrder }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityById(int id)
        {
            return Ok(await mediator.Send(new GetByIdCommand() { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] CityDetailDTO city)
        {
            try
            {
                return Created("", await mediator.Send(new AddCityCommand() { City = city }));
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCity(int id)
        {
            try
            {
                await mediator.Send(new DeleteCityCommand { Id = id });
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateCity(int id, [FromBody] CityUpdateDTO updatedCity)
        {
            if (id != updatedCity.Id)
                return BadRequest("ID in URL does not match ID in body.");

            try
            {
                var result = await mediator.Send(new UpdateCityCommand { City = updatedCity });
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
