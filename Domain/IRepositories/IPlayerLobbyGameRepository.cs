using System.Collections.Generic;
using System.Threading.Tasks;
using UnityArenaApi.Domain.Models;
namespace UnityArenaApi.Domain.IRepositories
{
    public interface IPlayerLobbyGameRepository
    {
        Task<IEnumerable<PlayerLobbyGame>> GetAllAsync();
        Task AddAsync(PlayerLobbyGame playerLobbyGame);
        void Update(PlayerLobbyGame playerLobbyGame);
        Task<PlayerLobbyGame> FindByCompatibleKeyAsync(int playerId,int lobbyGameId);
        Task<IEnumerable<PlayerLobbyGame>> GetByPlayerId(int playerId);
        Task<IEnumerable<PlayerLobbyGame>> GetByLobbyGameId(int lobbyGameid);
        void Delete(PlayerLobbyGame playerLobbyGame);
    }
}