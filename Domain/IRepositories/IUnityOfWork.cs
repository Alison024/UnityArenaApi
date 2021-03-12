using System.Threading.Tasks;
namespace UnityArenaApi.Domain.IRepositories
{
    public interface IUnityOfWork
    {
        Task CompleteAsync();
    }
}