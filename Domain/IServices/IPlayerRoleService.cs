using System.Threading.Tasks;
using System.Collections.Generic;
using UnityArenaApi.Domain.Models;
using UnityArenaApi.Domain.Responses;
namespace UnityArenaApi.Domain.IServices
{
    public interface IPlayerRoleService
    {
        Task<IEnumerable<PlayerRole>> GetAllAsync();
        Task<IEnumerable<PlayerRole>> GetByPlayerIdAsync(int playerId);
        Task<IEnumerable<PlayerRole>> GetByRoleIdAsync(int roleId);
        Task<PlayerRole> GetByCompatibleKeyAsync(int playerId,int roleId);
        Task<GenericResponse<PlayerRole>> SaveAsync(PlayerRole playerRole);
        //Task<GenericResponse<PlayerRole>> UpdateAsync(PlayerRole playerRole);
        Task<GenericResponse<PlayerRole>> DeleteAsync(PlayerRole playerRole);
    }
}