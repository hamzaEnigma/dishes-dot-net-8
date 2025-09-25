using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.RestaurantById;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RestaurantsController(
        IValidator<CreateRestaurantCommand> _validatorCreateCommand,
        IValidator<UpdateRestaurantCommand> _validatorUpdateCommand,
        IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll([FromQuery] GetAllRestaurantsQuery getAllRestaurantsQuery)
        {
            var resturants = await _mediator.Send(getAllRestaurantsQuery);
            return Ok(resturants);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<RestaurantDto>> GetById([FromRoute] Guid id)
        {
            var restaurant = await _mediator.Send(new GetRestaurantByIdQuery(id));

            return Ok(restaurant);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
           await _mediator.Send(new DeleteRestaurantCommand(id));
            return NoContent();
        }
        
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]


        public async Task<IActionResult> Update([FromBody] UpdateRestaurantCommand updateRestaurantCommand)
        {
            var result = await _validatorUpdateCommand.ValidateAsync(updateRestaurantCommand);

            if (result.IsValid)
            {
               await _mediator.Send(updateRestaurantCommand);
                    return NoContent();
            }
            return BadRequest(result.Errors);
        }
    }
}
