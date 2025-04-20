using BE.Domain.Entities;
using BE.Domain.Repositories;
using BE.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BE.Infrastructure.Repositories
{
    internal class ImmobilienHausgeldRepository(ImmobilienDbContext dbContext) : IImmobilienHausgeldRepository
    {
        public async Task<int> Create(ImmobilienHausgeld entity)
        {
            dbContext.ImmobilienHausgeld.Add(entity);
            await dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task Delete(ImmobilienHausgeld entity)
        {
            dbContext.ImmobilienHausgeld.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ImmobilienHausgeld>> GetAllAsync()
        {
            var hausgeld = await dbContext.ImmobilienHausgeld.ToListAsync();

            return hausgeld;
        }

        public async Task<ImmobilienHausgeld?> GetByIdAsync(int id)
        {
            var hausgeld = await dbContext.ImmobilienHausgeld
                .FirstOrDefaultAsync(x => x.Id == id);

            return hausgeld;
        }

        public async Task SaveChanges()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
