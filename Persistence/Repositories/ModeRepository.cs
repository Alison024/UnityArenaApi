using UnityArenaApi.Persistence.Context;
using UnityArenaApi.Domain.IRepositories;
using System.Threading.Tasks;
using UnityArenaApi.Domain.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace UnityArenaApi.Persistence.Repositories
{
    public class ModeRepository : BaseRepository, IModeRepository
    {
        public ModeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Mode mode)
        {
            await context.Modes.AddAsync(mode);
        }

        public void Delete(Mode mode)
        {
            context.Modes.Remove(mode);
        }

        public async Task<Mode> FindByIdAsync(int id)
        {
            return await context.Modes.FindAsync(id);
        }

        public async Task<IEnumerable<Mode>> GetAllAsync()
        {
            return await context.Modes.ToListAsync();
        }

        public void Update(Mode mode)
        {
            context.Modes.Update(mode);
        }
    }
}