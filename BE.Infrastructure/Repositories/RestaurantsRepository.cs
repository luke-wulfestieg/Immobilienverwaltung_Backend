using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Domain.Entities;
using BE.Domain.Repositories;
using BE.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BE.Infrastructure.Repositories
{
    internal class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantRepository
    {
        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await dbContext.Restaurants.ToListAsync();

            return restaurants;
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            var restaurant = await dbContext.Restaurants
                .Include(r => r.Dishes)
                .FirstOrDefaultAsync(x => x.Id == id);

            return restaurant;
        }
        public async Task<int> Create(Restaurant entitiy)
        {
            dbContext.Restaurants.Add(entitiy);
            await dbContext.SaveChangesAsync();

            return entitiy.Id;
        }

        public async Task Delete(Restaurant entity)
        {
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task SaveChanges() => dbContext.SaveChangesAsync();
    }
}
