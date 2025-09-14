using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domin.Entities;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController(IRestaurantService _restaurantsService, IValidator<CreateRestaurantDto> _validatorreateRestaurantDto) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var resturants = await _restaurantsService.GetAll();
            return Ok(resturants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var restaurant = await _restaurantsService.GetById(id);
            if (restaurant == null)
                return NotFound();
            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRestaurantDto createRestaurantDto)
        {
            var result = await _validatorreateRestaurantDto.ValidateAsync(createRestaurantDto);

            if (result.IsValid)
            {
                var restaurantId = await _restaurantsService.Create(createRestaurantDto);
                return CreatedAtAction(nameof(GetById), new { id = restaurantId }, null);
            }
            return BadRequest(result);

        }
    }
}
