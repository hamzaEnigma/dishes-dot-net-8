using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Users.Commands;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController(IMediator _mediator) : ControllerBase
    {
        [HttpPatch("user")]
        [Authorize]

        public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateRestaurantCommand)
        {
            await _mediator.Send(updateRestaurantCommand);
            return NoContent(); 
        }

    }
}
