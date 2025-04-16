using BE.Application.Dishes.Commands.CreateDish;
using BE.Application.Dishes.DTOs;
using BE.Application.Dishes.Queries.GetDishesForRestaurant;
using BE.Application.Dishes.Queries.GetDishById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BE.Application.Dishes.Commands.DeleteAllDishes;

namespace Immobilienverwaltung_Backend.Controllers
{
    [ApiController]
    [Route("api/restaurant/{restaurantId}/dishes")]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant([FromRoute] int restaurantId)
        {
            var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));

            return Ok(dishes);
        }

        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishDto>> GetByIdForRestaurant([FromRoute] int restaurantId, [FromRoute]int dishId)
        {
            var dish = await mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));

            return Ok(dish);
        }


        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute]int restaurantId, CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;

            var dishId = await mediator.Send(command);
            
            return CreatedAtAction(nameof(GetByIdForRestaurant), new { restaurantId, dishId }, null);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllDishesFromRestaurant([FromRoute] int restaurantId)
        {
            await mediator.Send(new DeleteAllDishesFromRestaurantCommand(restaurantId));

            return NoContent();
        }

        //[HttpDelete("{dishId}")]
        //public async Task<IActionResult> DeleteDishesById([FromRoute]int restaurantId, [FromRoute] int dishId)
        //{

        //}
    }
}
