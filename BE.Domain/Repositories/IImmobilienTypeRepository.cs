using BE.Domain.Entities;

namespace BE.Domain.Repositories
{
    public interface IImmobilienTypeRepository
    {
        Task<bool> IsInUseAsync(int id);
        Task<IEnumerable<ImmobilienType>> GetAllAsync();
        Task<ImmobilienType?> GetByIdAsync(int id);
        Task<int> Create(ImmobilienType entity);
        Task Delete(ImmobilienType entity);
        Task SaveChanges();
    }
}
