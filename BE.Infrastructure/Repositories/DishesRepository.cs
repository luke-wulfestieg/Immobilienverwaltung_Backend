using BE.Domain.Entities;
using BE.Domain.Repositories;
using BE.Infrastructure.Persistence;

namespace BE.Infrastructure.Repositories
{
    internal class DishesRepository(RestaurantsDbContext dbContext) : IDishesRepository
    {
        public async Task<int> Create(Dish entity)
        {
            dbContext.Dishes.Add(entity);
            await dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task DeleteAll(IEnumerable<Dish> entities)
        {
            dbContext.Dishes.RemoveRange(entities);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteById(Dish entitiy)
        {
            dbContext.Dishes.Remove(entitiy);
            await dbContext.SaveChangesAsync();
        }
    }
}
