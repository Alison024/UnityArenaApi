using System.Collections.Generic;
using System.Threading.Tasks;
using UnityArenaApi.Domain.Models;
namespace UnityArenaApi.Domain.IRepositories
{
    public interface IPlayerRoleRepository
    {
        Task<IEnumerable<PlayerRole>> GetAllAsync();
        Task AddAsync(PlayerRole playerRole);
        void Update(PlayerRole playerRole);
        Task<PlayerRole> FindByCompatibleKeyAsync(int playerId,int roleId);
        Task<IEnumerable<PlayerRole>> GetByPlayerId(int playerId);
        Task<IEnumerable<PlayerRole>> GetByRoleId(int roleId);
        void Delete(PlayerRole playerRole);
    }
}