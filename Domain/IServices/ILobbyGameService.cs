using System.Threading.Tasks;
using System.Collections.Generic;
using UnityArenaApi.Domain.Models;
using UnityArenaApi.Domain.Responses;
namespace UnityArenaApi.Domain.IServices
{
    public interface ILobbyGameService
    {
        Task<IEnumerable<LobbyGame>> GetAllAsync();
        Task<LobbyGame> GetById(int id);
        Task<GenericResponse<LobbyGame>> SaveAsync(LobbyGame lobbyGame);
        Task<GenericResponse<LobbyGame>> UpdateAsync(LobbyGame lobbyGame);
        Task<GenericResponse<LobbyGame>> DeleteAsync(int id);
    }
}