using UnityArenaApi.Persistence.Context;
using UnityArenaApi.Domain.IRepositories;
using System.Threading.Tasks;
using UnityArenaApi.Domain.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace UnityArenaApi.Persistence.Repositories
{
    public class PlayerRoleRepository : BaseRepository, IPlayerRoleRepository
    {
        public PlayerRoleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(PlayerRole playerRole)
        {
            await context.PlayerRoles.AddAsync(playerRole);
        }

        public void Delete(PlayerRole playerRole)
        {
            context.PlayerRoles.Remove(playerRole);
        }

        public async Task<PlayerRole> FindByCompatibleKeyAsync(int playerId, int roleId)
        {
            return await context.PlayerRoles.SingleOrDefaultAsync(x=>x.PlayerId==playerId && x.RoleId==roleId);
        }

        public async Task<IEnumerable<PlayerRole>> GetAllAsync()
        {
            return await context.PlayerRoles.ToListAsync();
        }

        public async Task<IEnumerable<PlayerRole>> GetByPlayerId(int playerId)
        {
            return await context.PlayerRoles.Where(x=>x.PlayerId==playerId).ToListAsync();
        }

        public async Task<IEnumerable<PlayerRole>> GetByRoleId(int roleId)
        {
            return await context.PlayerRoles.Where(x=>x.RoleId==roleId).ToListAsync();
        }

        public void Update(PlayerRole playerRole)
        {
            context.Update(playerRole);
        }
    }
}