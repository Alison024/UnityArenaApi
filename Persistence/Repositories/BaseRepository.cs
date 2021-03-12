using UnityArenaApi.Persistence.Context;
namespace UnityArenaApi.Persistence.Repositories
{
    public class BaseRepository
    {
        protected readonly AppDbContext context;
        public BaseRepository(AppDbContext context){
            this.context = context;
        }
    }
}