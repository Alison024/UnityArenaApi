using System.Collections.Generic;
using System.Threading.Tasks;
using UnityArenaApi.Domain.Models;
namespace UnityArenaApi.Domain.IRepositories
{
    public interface ILobbyGameRepository
    {
        Task<IEnumerable<LobbyGame>> GetAllAsync();
        Task AddAsync(LobbyGame lobbyGame);
        void Update(LobbyGame lobbyGame);
        Task<LobbyGame> FindByIdAsync(int id);
        void Delete(LobbyGame lobbyGame);
    }
}