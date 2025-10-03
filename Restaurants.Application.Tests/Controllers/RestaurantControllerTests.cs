
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Restaurants.Application.Tests.Controllers
{
    public class RestaurantControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public RestaurantControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact()]
        public async Task GetAll_ForValidRequest_Returns200Ok()
        {
            // arrange
            var client = _factory.CreateClient();


            // act

            var result = await client.GetAsync("/api/Restaurants");


            // assert

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact()]
        public async Task GetAll_ForNotValidRequest_Returns400BadRequest()
        {
            // arrange
            var client = _factory.CreateClient();


            // act

            var result = await client.GetAsync("/api/Restaurant/all");


            // assert

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }


    }
}
