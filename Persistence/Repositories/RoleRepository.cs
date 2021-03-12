using System.Data;
using UnityArenaApi.Persistence.Context;
using UnityArenaApi.Domain.IRepositories;
using System.Threading.Tasks;
using UnityArenaApi.Domain.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace UnityArenaApi.Persistence.Repositories
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Role role)
        {
            await context.Roles.AddAsync(role);
        }

        public void Delete(Role role)
        {
            context.Roles.Remove(role);
        }

        public async Task<Role> FindByIdAsync(int id)
        {
            return await context.Roles.FindAsync(id);
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await context.Roles.ToListAsync();
        }

        public void Update(Role role)
        {
            context.Roles.Update(role);
        }
    }
}