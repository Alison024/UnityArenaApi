using UnityArenaApi.Persistence.Context;
using UnityArenaApi.Domain.IRepositories;
using System.Threading.Tasks;
using UnityArenaApi.Domain.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace UnityArenaApi.Persistence.Repositories
{
    public class PlayerRepository : BaseRepository, IPlayerRepository
    {
        public PlayerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Player player)
        {
            player.PlayerRoles.Add(new PlayerRole{RoleId = 1});
            await context.Players.AddAsync(player);
        }

        public void Delete(Player player)
        {
            context.Players.Remove(player);
        }

        public async Task<Player> FindByIdAsync(int id)
        {
            return await context.Players.Include(x=>x.PlayerInfo).Include(x=>x.PlayerRoles).ThenInclude(x=>x.Role).SingleOrDefaultAsync(x=>x.Id ==id);
        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            return await context.Players.Include(x=>x.PlayerInfo).Include(x=>x.PlayerRoles).ThenInclude(x=>x.Role).ToListAsync();
        }

        public void Update(Player player)
        {
            context.Players.Update(player);
        }
    }
}