using UnityArenaApi.Persistence.Context;
using UnityArenaApi.Domain.IRepositories;
using System.Threading.Tasks;
using UnityArenaApi.Domain.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace UnityArenaApi.Persistence.Repositories
{
    public class PlayerLobbyGameRepository : BaseRepository, IPlayerLobbyGameRepository
    {
        public PlayerLobbyGameRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(PlayerLobbyGame playerLobbyGame)
        {
            await context.PlayerLobbyGames.AddAsync(playerLobbyGame);
        }

        public void Delete(PlayerLobbyGame playerLobbyGame)
        {
            context.PlayerLobbyGames.Remove(playerLobbyGame);
        }

        public async Task<PlayerLobbyGame> FindByCompatibleKeyAsync(int playerId, int lobbyGameId)
        {
            return await context.PlayerLobbyGames.SingleOrDefaultAsync(x=>x.PlayerId==playerId && x.LobbyGameId==lobbyGameId);
        }

        public async Task<IEnumerable<PlayerLobbyGame>> GetAllAsync()
        {
            return await context.PlayerLobbyGames.ToListAsync();
        }

        public async Task<IEnumerable<PlayerLobbyGame>> GetByLobbyGameId(int lobbyGameid)
        {
            return await context.PlayerLobbyGames.Where(x=>x.LobbyGameId ==lobbyGameid).ToListAsync();
        }

        public async Task<IEnumerable<PlayerLobbyGame>> GetByPlayerId(int playerId)
        {
            return await context.PlayerLobbyGames.Where(x=>x.PlayerId ==playerId).ToListAsync();
        }

        public void Update(PlayerLobbyGame playerLobbyGame)
        {
            context.PlayerLobbyGames.Update(playerLobbyGame);
        }
    }
}