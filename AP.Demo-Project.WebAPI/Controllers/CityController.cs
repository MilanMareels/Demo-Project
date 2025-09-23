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
        public IActionResult UpdateCity(int id, [FromBody] City updatedCity)
        {
            if (id != updatedCity.Id) return BadRequest();

            try
            {
                // Hier de update!
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
