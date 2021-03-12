using UnityArenaApi.Persistence.Context;
using UnityArenaApi.Domain.IRepositories;
using System.Threading.Tasks;
using UnityArenaApi.Domain.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UnityArenaApi.Persistence.Repositories
{
    public class UnitOfWork : BaseRepository, IUnityOfWork
    {
        public UnitOfWork(AppDbContext context) : base(context)
        {
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}