using System.Threading.Tasks;
using System.Collections.Generic;
using UnityArenaApi.Domain.Models;
using UnityArenaApi.Domain.Responses;
namespace UnityArenaApi.Domain.IServices
{
    public interface IPlayerLobbyGameService
    {
         
        Task<IEnumerable<PlayerLobbyGame>> GetAllAsync();
        Task<IEnumerable<PlayerLobbyGame>> GetByPlayerIdAsync(int playerId);
        Task<IEnumerable<PlayerLobbyGame>> GetByLobbyGameIdAsync(int lobbyGameId);
        Task<PlayerLobbyGame> GetByCompatibleKeyAsync(int playerId,int lobbyId);
        Task<GenericResponse<PlayerLobbyGame>> SaveAsync(PlayerLobbyGame playerLobbyGame);
        //Task<GenericResponse<PlayerLobbyGame>> UpdateAsync(PlayerLobbyGame playerLobbyGame);
        Task<GenericResponse<PlayerLobbyGame>> DeleteAsync(PlayerLobbyGame playerLobbyGame);
    }
}