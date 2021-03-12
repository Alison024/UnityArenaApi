using System.Collections.Generic;
using System.Threading.Tasks;
using UnityArenaApi.Domain.Models;
namespace UnityArenaApi.Domain.IRepositories
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> GetAllAsync();
        Task AddAsync(Player player);
        void Update(Player player);
        Task<Player> FindByIdAsync(int id);
        void Delete(Player player);
    }
}