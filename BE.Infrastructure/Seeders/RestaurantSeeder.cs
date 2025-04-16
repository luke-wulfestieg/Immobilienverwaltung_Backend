using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Domain.Entities;
using BE.Infrastructure.Persistence;

namespace BE.Infrastructure.Seeders
{
    internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();

                    dbContext.AddRange(restaurants);
                    await dbContext.SaveChangesAsync();
                }
            }
        }


        private IEnumerable<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = [new() {
                Name = "TestRestaurant",
                Category = "Fast Food",
                Description = "Kolinski Foods",
                ContactEmail = "kol@kol.de",
                ContactNumber  = "55517416546",
                HasDelivery = true,
                Dishes = [new() {
                    Name = "NUggets",
                    Description = "Hühnchen",
                    Price = 5.95M,
                    KiloCalories = 160,
                },
                new() {
                    Name = "Pommes",
                    Description = "Kartoffeln",
                    Price = 2.95M
                }],
                Address = new() {
                    City = "Hamburg",
                    PostalCode = "59152",
                    Street = "Nebenplatz 5"
                }
            }];

            return restaurants;
        }

    }
}
