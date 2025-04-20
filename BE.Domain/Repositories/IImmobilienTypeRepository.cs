using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Domain.Entities;

namespace BE.Domain.Repositories
{
    public interface IImmobilienTypeRepository
    {
        Task<IEnumerable<ImmobilienType>> GetAllAsync();
        Task<ImmobilienType?> GetByIdAsync(int id);
        Task<int> Create(ImmobilienType entity);
        Task Delete(ImmobilienType entity);
        Task SaveChanges();
    }
}
