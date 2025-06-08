using BE.Domain.Entities;
using BE.Domain.Repositories;
using BE.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BE.Infrastructure.Repositories
{
    internal class ImmobilienOverviewRepository(ImmobilienDbContext dbContext) : IImmobilienOverviewRepository
    {
        public async Task<int> Create(ImmobilienOverview entity)
        {
            dbContext.ImmobilienOverviews.Add(entity);
            await dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task Delete(ImmobilienOverview entity)
        {
            dbContext.ImmobilienOverviews.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ImmobilienOverview>> GetAllAsync()
        {
            var overviews = await dbContext.ImmobilienOverviews
                .Include(o => o.ImmobilienType)
                .Include(o => o.ImmobilienHausgeld)
                .Include(o => o.ImmobilienHypothek)
                .Include(o => o.Bruttomietrendite)
                .Include(o => o.Ruecklage)
                .Include(o => o.Gesamtbelastung).ToListAsync();


            return overviews;
        }

        public async Task<ImmobilienOverview?> GetByIdAsync(int id)
        {
            var overview = await dbContext.ImmobilienOverviews
                .Include(r => r.ImmobilienType)
                .Include(r => r.ImmobilienHausgeld)
                .Include(r => r.ImmobilienHypothek)
                .Include(o => o.Bruttomietrendite)
                .Include(o => o.Ruecklage)
                .Include(o => o.Gesamtbelastung)
                .FirstOrDefaultAsync(x => x.Id == id);

            return overview;
        }

        public async Task SaveChanges()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
