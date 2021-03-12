using System.Threading.Tasks;
using System.Collections.Generic;
using UnityArenaApi.Domain.Models;
using UnityArenaApi.Domain.Responses;
namespace UnityArenaApi.Domain.IServices
{
    public interface IPlayerService
    {
        Task<IEnumerable<Player>> GetAllAsync();
        Task<Player> GetById(int id);
        Task<GenericResponse<Player>> SaveAsync(Player player);
        Task<GenericResponse<Player>> UpdateAsync(Player player);
        Task<GenericResponse<Player>> DeleteAsync(int id);
    }
}