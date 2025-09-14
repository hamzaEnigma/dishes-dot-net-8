
using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Validators
{
    public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
    {
        private readonly List<string> validCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];


        public CreateRestaurantDtoValidator()
        {
            RuleFor(dto => dto.Name).Length(3, 100);
            RuleFor(dto => dto.Category)
              .Must(validCategories.Contains)
              .WithMessage("Invalid category. Please choose from the valid categories: \"Italian\", \"Mexican\", \"Japanese\", \"American\", \"Indian\"");
            RuleFor(dto => dto.ContactEmail)
            .EmailAddress()
            .WithMessage("Please provide a valid email address");
        }
    }
}
