using AP.Demo_Project.Application.CQRS.City;
using AP.Demo_Project.Application.Interfaces;
using AP.Demo_Project.Domain;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AP.Demo_Project.WebAPI.Controllers
{
    public class CityController : APIv1Controller
    {
        private readonly ICityService cityService;
        private readonly IMediator mediator;

        public CityController(ICityService cityService, IMediator mediator) 
        {
            this.cityService = cityService;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities([FromQuery] int pageNr = 1, [FromQuery] int pageSize = 5, [FromQuery] string sortBy = "Population", [FromQuery] string sortOrder = "asc")
        {
           //return Ok(await cityService.GetAll(pageNr, pageSize, sortBy, sortOrder));
            return Ok(await mediator.Send(new GetAllCitiesQuery() { PageNr = pageNr, PageSize = pageSize, SortBy = sortBy, SortOrder = sortOrder }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] CityDetailDTO city)
        {
           try
           {
                return Created("", await mediator.Send(new AddCityCommand() { City = city }));
                //return Created("", await cityService.Add(city));
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
            // Hier de delete!
            return NoContent();
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateCity(int id, [FromBody] CityUpdateDTO updatedCity)
        {
            if (id != updatedCity.Id)
                return BadRequest("ID in URL does not match ID in body.");

            try
            {
                var result = await mediator.Send(new UpdateCityCommand { City = updatedCity });// Call service
                return Ok(result); // Return updated city
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // City not found
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Validation errors (e.g., duplicate name)
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Any other server errors
            }
        }
    }
}
