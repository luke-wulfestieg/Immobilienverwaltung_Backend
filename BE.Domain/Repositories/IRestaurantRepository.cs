using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Domain.Entities;

namespace BE.Domain.Repositories
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<Restaurant?> GetByIdAsync(int id);
        Task<int> Create(Restaurant entity);
        Task Delete(Restaurant entity);

        Task SaveChanges();
    }
}
