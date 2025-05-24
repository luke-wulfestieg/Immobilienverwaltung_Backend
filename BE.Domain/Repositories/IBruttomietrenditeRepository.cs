using BE.Domain.Entities;

namespace BE.Domain.Repositories
{
    public interface IBruttomietrenditeRepository
    {
        Task<IEnumerable<Bruttomietrendite>> GetAllAsync();
        Task<Bruttomietrendite?> GetByIdAsync(int id);
        Task<int> Create(Bruttomietrendite entity);
        Task Delete(Bruttomietrendite entity);
        Task SaveChanges();
    }
}
