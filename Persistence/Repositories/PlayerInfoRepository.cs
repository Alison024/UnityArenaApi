using UnityArenaApi.Persistence.Context;
using UnityArenaApi.Domain.IRepositories;
using System.Threading.Tasks;
using UnityArenaApi.Domain.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace UnityArenaApi.Persistence.Repositories
{
    public class PlayerInfoRepository : BaseRepository, IPlayerInfoRepository
    {
        public PlayerInfoRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(PlayerInfo playerInfo)
        {
            await context.PlayersInfo.AddAsync(playerInfo);
        }

        public void Delete(PlayerInfo playerInfo)
        {
            context.PlayersInfo.Remove(playerInfo);
        }

        public async Task<PlayerInfo> FindByIdAsync(int id)
        {
            return await context.PlayersInfo.FindAsync(id);
        }

        public async Task<IEnumerable<PlayerInfo>> GetAllAsync()
        {
            return await context.PlayersInfo.ToListAsync();
        }

        public void Update(PlayerInfo playerInfo)
        {
            context.PlayersInfo.Update(playerInfo);
        }
    }
}