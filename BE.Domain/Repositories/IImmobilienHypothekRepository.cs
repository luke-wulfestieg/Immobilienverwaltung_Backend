using BE.Domain.Entities.Hypothek;

namespace BE.Domain.Repositories
{
    public interface IImmobilienHypothekRepository
    {
        Task<IEnumerable<ImmobilienHypothek>> GetAllAsync();
        Task<ImmobilienHypothek?> GetByIdAsync(int id);
        Task<int> Create(ImmobilienHypothek entity);
        Task Delete(ImmobilienHypothek entity);
        Task SaveChanges();
    }
}
