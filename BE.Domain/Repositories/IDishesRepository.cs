using BE.Domain.Entities;

namespace BE.Domain.Repositories
{
    public interface IDishesRepository
    {
        Task<int> Create(Dish entity);
        Task DeleteAll(IEnumerable<Dish> entities);

        Task DeleteById(Dish entitiy);

    }
}
