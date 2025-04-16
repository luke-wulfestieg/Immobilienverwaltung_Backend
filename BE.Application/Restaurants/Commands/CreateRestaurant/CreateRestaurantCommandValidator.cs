using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Application.Restaurants.DTOs;
using FluentValidation;

namespace BE.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        public CreateRestaurantCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(1, 100);

            RuleFor(dto => dto.ContactEmail)
               .EmailAddress()
               .WithMessage("Gib eine gültige Email an");

            RuleFor(dto => dto.PostalCode)
               .Length(5, 5)
               .WithMessage("Gib eine gültige PLZ an - (xxxxx)");

        }
    }
}
