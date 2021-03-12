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
            await context.Players.AddAsync(player);
        }

        public void Delete(Player player)
        {
            context.Players.Remove(player);
        }

        public async Task<Player> FindByIdAsync(int id)
        {
            return await context.Players.FindAsync(id);
        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            return await context.Players.ToListAsync();
        }

        public void Update(Player player)
        {
            context.Players.Update(player);
        }
    }
}