using BE.Domain.Entities;
using BE.Domain.Repositories;
using BE.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BE.Infrastructure.Repositories
{
    internal class GesamtbelastungRepository(ImmobilienDbContext dbContext) : IGesamtbelastungRepository
    {
        public async Task<int> Create(Gesamtbelastung entity)
        {
            dbContext.Gesamtbelastungen.Add(entity);
            await dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task Delete(Gesamtbelastung entity)
        {
            dbContext.Gesamtbelastungen.Remove(entity);
            await dbContext.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<Gesamtbelastung>> GetAllAsync()
        {
            var gesamtbelastungen = await dbContext.Gesamtbelastungen.ToListAsync();

            return gesamtbelastungen;
        }

        public async Task<Gesamtbelastung?> GetByIdAsync(int id)
        {
            var gesamtbelastung = await dbContext.Gesamtbelastungen.FirstOrDefaultAsync(x => x.Id == id);

            return gesamtbelastung;
        }

        public async Task SaveChanges()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
