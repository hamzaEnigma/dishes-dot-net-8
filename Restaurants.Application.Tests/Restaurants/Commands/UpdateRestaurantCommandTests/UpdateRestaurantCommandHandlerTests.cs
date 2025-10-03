using AutoMapper;
using Azure.Core;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domin.Constants;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Exceptions;
using Restaurants.Domin.Interfaces;
using Restaurants.Domin.Repositories;
using Xunit;

namespace Restaurants.Application.Tests.Restaurants.Commands.UpdateRestaurantCommandTests
{
    public class UpdateRestaurantCommandHandlerTests
    {
        private readonly Mock<IRestaurantsRepository> _restaurantsRepoMock;
        private readonly UpdateRestaurantCommandHandler _handler;
        private readonly Mock<IRestaurantAuthorizationService> _authRestoServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<UpdateRestaurantCommand>> _loggerMock;


        public UpdateRestaurantCommandHandlerTests()
        {
            _loggerMock = new Mock<ILogger<UpdateRestaurantCommand>>();
            _restaurantsRepoMock = new Mock<IRestaurantsRepository>();
            _authRestoServiceMock = new Mock<IRestaurantAuthorizationService>();
            _mapperMock = new Mock<IMapper>();
            _handler = new UpdateRestaurantCommandHandler(_mapperMock.Object, _restaurantsRepoMock.Object,
                _authRestoServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task  Handle_WithValidRequest_ShouldUpdateRestaurants()
        {
            //arrange
            var restoUpdateCommand = new UpdateRestaurantCommand
            {
                Id = Guid.NewGuid(),
                Name = "Updated Name",
                Description = "Updated Description",
                HasDelivery = true
            };

            var restaurant = new Restaurant()
            {
                Id = restoUpdateCommand.Id,
                Name = "Old Name",
                Description = "Old Description",
                HasDelivery = false
            };

            _restaurantsRepoMock.Setup(r => r.GetByIdAsync(restoUpdateCommand.Id))
            .ReturnsAsync(restaurant);

            _authRestoServiceMock.Setup(a => a.Authorize(restaurant, ResourceOperation.Update)).Returns(true);

            //act
            await _handler.Handle(restoUpdateCommand, CancellationToken.None);

            //assert

            _restaurantsRepoMock.Verify(r=>r.SaveChangesAsync(), Times.Once);
            _mapperMock.Verify(m => m.Map(restoUpdateCommand, restaurant), Times.Once);


        }

        [Fact]
        public async Task Handle_WithNoRestaurantFound_ShouldThrowError()
        {
            //arrange
            var restaurantId = new Guid();


            var restoUpdateCommand = new UpdateRestaurantCommand
            {
                Id = restaurantId,
            };


            _restaurantsRepoMock.Setup(r => r.GetByIdAsync(restoUpdateCommand.Id))
            .ReturnsAsync((Restaurant?)null);


            //act
            Func<Task> act = async () => await _handler.Handle(restoUpdateCommand, CancellationToken.None);

            //assert

            await act.Should().ThrowAsync<NotFoundException>()
                    .WithMessage($"Restaurant with id {restaurantId} doen't exist");

        }

        [Fact]
        public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidException()
        {
            //arrange
            var restoUpdateCommand = new UpdateRestaurantCommand
            {
                Id = Guid.NewGuid(),
                Name = "Updated Name",
                Description = "Updated Description",
                HasDelivery = true
            };

            var restaurant = new Restaurant()
            {
                Id = restoUpdateCommand.Id,
                Name = "Old Name",
                Description = "Old Description",
                HasDelivery = false
            };

            _restaurantsRepoMock.Setup(r => r.GetByIdAsync(restoUpdateCommand.Id))
            .ReturnsAsync(restaurant);

            _authRestoServiceMock.Setup(a => a.Authorize(restaurant, ResourceOperation.Update)).Returns(false);

            //act
            Func<Task> act = async () =>  await _handler.Handle(restoUpdateCommand, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<ForbidenException>();
        }

    }
}
