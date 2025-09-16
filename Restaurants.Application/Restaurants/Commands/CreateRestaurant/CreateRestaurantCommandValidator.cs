using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        private readonly List<string> validCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];

        public CreateRestaurantCommandValidator()
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
