using BE.Domain.Entities;
using BE.Domain.Repositories;
using BE.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BE.Infrastructure.Repositories
{
    internal class ImmobilienTypeRepository(ImmobilienDbContext dbContext) : IImmobilienTypeRepository
    {
        public async Task<int> Create(ImmobilienType entity)
        {
            dbContext.ImmobilienTypes.Add(entity);
            await dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task Delete(ImmobilienType entity)
        {
            dbContext.ImmobilienTypes.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ImmobilienType>> GetAllAsync()
        {
            var types = await dbContext.ImmobilienTypes.ToListAsync();

            return types;
        }

        public async Task<ImmobilienType?> GetByIdAsync(int id)
        {
            var type = await dbContext.ImmobilienTypes
                .FirstOrDefaultAsync(x => x.Id == id);

            return type;
        }

        public async Task<bool> IsInUseAsync(int id)
        {
            return await dbContext.ImmobilienOverviews.AnyAsync(i => i.ImmobilienType.Id == id);
        }
        public async Task SaveChanges()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
