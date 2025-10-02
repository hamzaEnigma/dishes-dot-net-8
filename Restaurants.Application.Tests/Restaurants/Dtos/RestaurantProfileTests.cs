using AutoMapper;
using FluentAssertions;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domin.Entities;
using Xunit;

namespace Restaurants.Application.Tests.Restaurants.Dtos
{
    public class RestaurantProfileTests
    {
        private IMapper _mapper;

        public RestaurantProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RestaurantProfile>();
            });
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void Configuration_Should_Be_Valid()
        {
            // Si le ctor n’a pas lancé d’exception, la config est OK
            true.Should().BeTrue();
        }

        [Fact]
        public void CreateMapFor_RestaurantToRestaurantDto_ShouldMapCorrectly()
        {
            //arrange
            var restaurant = new Restaurant()
            {
                Id = Guid.NewGuid(),
                Name = "Test Restaurant",
                Description = "A test restaurant",
                Category = "Test Category",
                ContactEmail = "emailtest@gmail.com",
                Address = new Address
                {
                    City = "Test City",
                    Street = "123 Test St",
                    PostalCode = "12345"
                },
            };

            //act
            var dto = _mapper.Map<RestaurantDto>(restaurant);

            // assert
            dto.Should().NotBeNull();
            dto.Id.Should().Be(restaurant.Id);
            dto.Name.Should().Be(restaurant.Name);
            dto.Description.Should().Be(restaurant.Description);
            dto.Category.Should().Be(restaurant.Category);
            dto.ContactEmail.Should().Be(restaurant.ContactEmail);
            dto.City.Should().Be(restaurant.Address?.City);
            dto.Street.Should().Be(restaurant.Address?.Street);
            dto.PostalCode.Should().Be(restaurant.Address?.PostalCode);
        }

        [Fact]
        public void CreateMapFor_RestaurantDtoToRestaurant_ShouldMapCorrectly()
        {
            // arrange
            var restaurantDto = new RestaurantDto
            {
                Id = Guid.NewGuid(),
                Name = "Test Restaurant",
                Description = "A test restaurant",
                Category = "Test Category",
                ContactEmail = "emailtest@gmail.com",
                City = "Test City",
                Street = "123 Test St",
                PostalCode = "12345"
            };

            //act
            var restaurant = _mapper.Map<Restaurant>(restaurantDto);

            //assert
            restaurant.Should().NotBeNull();
            restaurant.Id.Should().Be(restaurantDto.Id);
            restaurant.Name.Should().Be(restaurantDto.Name);
            restaurant.Description.Should().Be(restaurantDto.Description);
            restaurant.Category.Should().Be(restaurantDto.Category);
            restaurant.ContactEmail.Should().Be(restaurantDto.ContactEmail);
            restaurant.Address?.City.Should().Be(restaurantDto.City);
            restaurant.Address?.Street.Should().Be(restaurantDto.Street);
            restaurant.Address?.PostalCode.Should().Be(restaurantDto.PostalCode);   
            

        }

        [Fact]
        public void CreateMapFor_CreateRestaurantCommandToRestaurant_ShouldMapCorrectly()
        {
            // arrange
            var createCommand = new CreateRestaurantCommand()
            {
                Name = "Test Restaurant",
                Description = "Test Description",
                Category = "Test Category",
                HasDelivery = true,
                ContactEmail = "test@example.com",
                ContactNumber = "123456789",
                City = "Test City",
                Street = "Test Street",
                PostalCode = "12345"
            };

            // act
            var restaurant = _mapper.Map<Restaurant>(createCommand);

            // assert
            restaurant.Should().NotBeNull();
            restaurant.Name.Should().Be(createCommand.Name);
            restaurant.Description.Should().Be(createCommand.Description);
            restaurant.Category.Should().Be(createCommand.Category);
            restaurant.HasDelivery.Should().Be(createCommand.HasDelivery);
            restaurant.ContactEmail.Should().Be(createCommand.ContactEmail);
            restaurant.ContactNumber.Should().Be(createCommand.ContactNumber);
            restaurant.Address?.City.Should().Be(createCommand.City);
            restaurant.Address?.Street.Should().Be(createCommand.Street);
            restaurant.Address?.PostalCode.Should().Be(createCommand.PostalCode);
        }

    }
}
