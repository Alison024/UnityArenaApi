using System.Collections.Generic;
using System.Threading.Tasks;
using UnityArenaApi.Domain.Models;
namespace UnityArenaApi.Domain.IRepositories
{
    public interface IPlayerInfoRepository
    {
        Task<IEnumerable<PlayerInfo>> GetAllAsync();
        Task AddAsync(PlayerInfo playerInfo);
        void Update(PlayerInfo playerInfo);
        Task<PlayerInfo> FindByIdAsync(int id);
        void Delete(PlayerInfo playerInfo);
    }
}