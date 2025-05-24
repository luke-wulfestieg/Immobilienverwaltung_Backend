using BE.Domain.Entities;
using BE.Domain.Repositories;

using BE.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BE.Infrastructure.Repositories
{
    internal class BruttomietrenditeRepository(ImmobilienDbContext dbContext) : IBruttomietrenditeRepository
    {
        public async Task<int> Create(Bruttomietrendite entity)
        {
            dbContext.BruttoMietrenditen.Add(entity);
            await dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task Delete(Bruttomietrendite entity)
        {
            dbContext.BruttoMietrenditen.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Bruttomietrendite>> GetAllAsync()
        {
            var bruttomietrenditen = await dbContext.BruttoMietrenditen.ToListAsync();

            return bruttomietrenditen;
        }

        public async Task<Bruttomietrendite?> GetByIdAsync(int id)
        {
            var bruttomietrendite = await dbContext.BruttoMietrenditen
                .FirstOrDefaultAsync(x => x.Id == id);

            return bruttomietrendite;
        }

        public async Task SaveChanges()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
