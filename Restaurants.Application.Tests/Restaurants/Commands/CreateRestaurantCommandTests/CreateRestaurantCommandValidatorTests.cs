using FluentAssertions;
using FluentValidation.TestHelper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Xunit;

namespace Restaurants.Application.Tests.Restaurants.Commands.CreateRestaurantCommandTests
{
    public class CreateRestaurantCommandValidatorTests
    {
        [Fact]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            //arrang
            var command = new CreateRestaurantCommand()
            {
                Name = "Test",
                Category = "Italian",
                ContactEmail = "test@test.com",
                PostalCode = "12-345",
            };

            var validator = new CreateRestaurantCommandValidator();

            //act
            var result = validator.TestValidate(command);

            //assert
            result.ShouldNotHaveAnyValidationErrors();

        }
        [Fact]
        public void Validator_ForInValidCommand_ShouldHaveValidationErrors()
        {
            //arrang

            var command = new CreateRestaurantCommand()
            {
                Name = "re",
                Category = "tunisienne",
                ContactEmail = "tes",
                PostalCode = "12-345",
            };
            var validator = new CreateRestaurantCommandValidator();

            //act
            var result = validator.TestValidate(command);

            //assert
            result.ShouldHaveAnyValidationError();

        }
    }
}
