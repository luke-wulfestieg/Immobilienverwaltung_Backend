using BE.Domain.Entities;

namespace BE.Domain.Repositories
{
    public interface IRuecklagenRepository
    {
        Task<IEnumerable<Ruecklage>> GetAllAsync();
        Task<Ruecklage?> GetByIdAsync(int id);
        Task<int> Create(Ruecklage entity);
        Task Delete(Ruecklage entity);
        Task SaveChanges();
    }
}
