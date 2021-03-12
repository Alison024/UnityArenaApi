using System.Collections.Generic;
using System.Threading.Tasks;
using UnityArenaApi.Domain.Models;
namespace UnityArenaApi.Domain.IRepositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllAsync();
        Task AddAsync(Role role);
        void Update(Role role);
        Task<Role> FindByIdAsync(int id);
        void Delete(Role role);
    }
}