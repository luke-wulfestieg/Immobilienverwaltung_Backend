using BE.Domain.Entities;
using BE.Domain.Repositories;
using BE.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BE.Infrastructure.Repositories
{
    internal class RuecklagenRepository(ImmobilienDbContext dbContext) : IRuecklagenRepository
    {
        public async Task<int> Create(Ruecklage entity)
        {
            dbContext.Ruecklagen.Add(entity);
            await dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task Delete(Ruecklage entity)
        {
            dbContext.Ruecklagen.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Ruecklage>> GetAllAsync()
        {
            var hausgeld = await dbContext.Ruecklagen.ToListAsync();

            return hausgeld;
        }

        public async Task<Ruecklage?> GetByIdAsync(int id)
        {
            var hausgeld = await dbContext.Ruecklagen
                .FirstOrDefaultAsync(x => x.Id == id);

            return hausgeld;
        }

        public async Task SaveChanges()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
