using System.Collections.Generic;
using System.Threading.Tasks;
using UnityArenaApi.Domain.Models;
namespace UnityArenaApi.Domain.IRepositories
{
    public interface IModeRepository
    {
        Task<IEnumerable<Mode>> GetAllAsync();
        Task AddAsync(Mode mode);
        void Update(Mode mode);
        Task<Mode> FindByIdAsync(int id);
        void Delete(Mode mode);
    }
}