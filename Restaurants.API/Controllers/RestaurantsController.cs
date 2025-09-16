using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.RestaurantById;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController(
        IValidator<CreateRestaurantCommand> _validatorCreateCommand,
        IValidator<UpdateRestaurantCommand> _validatorUpdateCommand,
        IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var resturants = await _mediator.Send(new GetAllRestaurantsQuery());
            return Ok(resturants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var restaurant = await _mediator.Send(new GetRestaurantByIdQuery(id));
            if (restaurant == null)
                return NotFound();
            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRestaurantCommand createRestaurantCommand)
        {
            var result = await _validatorCreateCommand.ValidateAsync(createRestaurantCommand);

            if (result.IsValid)
            {
                var restaurantId = await _mediator.Send(createRestaurantCommand);
                return CreatedAtAction(nameof(GetById), new { id = restaurantId }, new { id = restaurantId });
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var res = await _mediator.Send(new DeleteRestaurantCommand(id));
            if (res)
                return NoContent();

            return NotFound();
        }
        
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateRestaurantCommand updateRestaurantCommand)
        {
            var result = await _validatorUpdateCommand.ValidateAsync(updateRestaurantCommand);

            if (result.IsValid)
            {
               var res =  await _mediator.Send(updateRestaurantCommand);
                if (res)
                {
                    return NoContent();
                }
                return NotFound();
            }
            return BadRequest(result.Errors);
        }
    }
}
