using UnityArenaApi.Persistence.Context;
using UnityArenaApi.Domain.IRepositories;
using System.Threading.Tasks;
using UnityArenaApi.Domain.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace UnityArenaApi.Persistence.Repositories
{
    public class LobbyGameRepository : BaseRepository,ILobbyGameRepository
    {
        public LobbyGameRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(LobbyGame lobbyGame)
        {
            await context.LobbyGames.AddAsync(lobbyGame);
        }

        public void Delete(LobbyGame lobbyGame)
        {
            context.LobbyGames.Remove(lobbyGame);
        }

        public async Task<LobbyGame> FindByIdAsync(int id)
        {
            return await context.LobbyGames.FindAsync(id);
        }

        public async Task<IEnumerable<LobbyGame>> GetAllAsync()
        {
            return await context.LobbyGames.ToListAsync();
        }

        public void Update(LobbyGame lobbyGame)
        {
            context.LobbyGames.Update(lobbyGame);
        }
    }
}