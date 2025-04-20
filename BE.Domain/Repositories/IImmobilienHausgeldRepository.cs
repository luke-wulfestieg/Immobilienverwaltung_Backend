using BE.Domain.Entities;

namespace BE.Domain.Repositories
{
    public interface IImmobilienHausgeldRepository
    {
        Task<IEnumerable<ImmobilienHausgeld>> GetAllAsync();
        Task<ImmobilienHausgeld?> GetByIdAsync(int id);
        Task<int> Create(ImmobilienHausgeld entity);
        Task Delete(ImmobilienHausgeld entity);
        Task SaveChanges();
    }
}
