using System.Threading.Tasks;
using System.Collections.Generic;
using UnityArenaApi.Domain.Models;
using UnityArenaApi.Domain.Responses;
namespace UnityArenaApi.Domain.IServices
{
    public interface IPlayerInfoService
    {
        Task<IEnumerable<PlayerInfo>> GetAllAsync();
        Task<PlayerInfo> GetById(int id);
        Task<GenericResponse<PlayerInfo>> SaveAsync(PlayerInfo playerInfo);
        Task<GenericResponse<PlayerInfo>> UpdateAsync(PlayerInfo playerInfo);
        Task<GenericResponse<PlayerInfo>> DeleteAsync(int id);
         
    }
}