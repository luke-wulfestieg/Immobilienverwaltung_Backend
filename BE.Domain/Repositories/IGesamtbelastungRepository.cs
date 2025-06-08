using BE.Domain.Entities;

namespace BE.Domain.Repositories
{
    public interface IGesamtbelastungRepository
    {
        Task<IEnumerable<Gesamtbelastung>> GetAllAsync();
        Task<Gesamtbelastung?> GetByIdAsync(int id);
        Task<int> Create(Gesamtbelastung entity);
        Task Delete(Gesamtbelastung entity);
        Task SaveChanges();
    }
}
