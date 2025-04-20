using BE.Domain.Entities;

namespace BE.Domain.Repositories
{
    public interface IImmobilienOverviewRepository
    {
        Task<IEnumerable<ImmobilienOverview>> GetAllAsync();
        Task<ImmobilienOverview?> GetByIdAsync(int id);
        Task<int> Create(ImmobilienOverview entity);
        Task Delete(ImmobilienOverview entity);
        Task SaveChanges();
    }
}
