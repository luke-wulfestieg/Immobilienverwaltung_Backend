using BE.Domain.Entities.Hypothek;
using BE.Domain.Repositories;
using BE.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BE.Infrastructure.Repositories
{
    internal class ImmobilienHypothekRepository(ImmobilienDbContext dbContext) : IImmobilienHypothekRepository
    {
        public async Task<int> Create(ImmobilienHypothek entity)
        {
            dbContext.ImmobilienHypotheken.Add(entity);
            await dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task Delete(ImmobilienHypothek entity)
        {
            dbContext.ImmobilienHypotheken.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ImmobilienHypothek>> GetAllAsync()
        {
            var hypotheken = await dbContext.ImmobilienHypotheken.ToListAsync();

            return hypotheken;
        }

        public async Task<ImmobilienHypothek?> GetByIdAsync(int id)
        {
            var hypothek = await dbContext.ImmobilienHypotheken.FirstOrDefaultAsync(x => x.Id == id);

            return hypothek;
        }

        public async Task SaveChanges()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
