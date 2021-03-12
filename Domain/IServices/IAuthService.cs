using System.Threading.Tasks;
using UnityArenaApi.Domain.Models;
namespace UnityArenaApi.Domain.IServices
{
    public interface IAuthService
    {
        Task<Player> Authenticate(string login, string password);
    }
}