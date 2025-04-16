using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace BE.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantbyIdValidator :AbstractValidator<UpdateRestaurantByIdCommand>
    {
        public UpdateRestaurantbyIdValidator()
        {
            RuleFor(c => c.Name)
                .Length(1,100);
        }
    }
}
