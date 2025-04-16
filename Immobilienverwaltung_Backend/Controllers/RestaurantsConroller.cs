using Microsoft.AspNetCore.Mvc;
using MediatR;
using BE.Application.Restaurants.Queries.GetAllRestaurants;
using BE.Application.Restaurants.Queries.GetRestaurantById;
using BE.Application.Restaurants.Commands.CreateRestaurant;
using BE.Application.Restaurants.Commands.DeleteRestaurant;
using BE.Application.Restaurants.Commands.UpdateRestaurant;
using BE.Application.Restaurants.DTOs;


namespace Immobilienverwaltung_Backend.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsConroller(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll() {
            var restaurants = await mediator.Send(new GetAllRestaurantQuery());
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDto>> GetById([FromRoute]int id)
        {
            var result = await mediator.Send(new GetRestaurantByIdQuery(id));

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand command)
        {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById([FromRoute] int id)
        {
            await mediator.Send(new DeleteRestaurantByIdCommand(id));

            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateById([FromRoute] int id, UpdateRestaurantByIdCommand command)
        {
            command.Id = id;    

            await mediator.Send(command);

            return NoContent(); 
        }

    }
}
