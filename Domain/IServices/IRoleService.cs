using System.Threading.Tasks;
using System.Collections.Generic;
using UnityArenaApi.Domain.Models;
using UnityArenaApi.Domain.Responses;
namespace UnityArenaApi.Domain.IServices
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role> GetById(int id);
        Task<GenericResponse<Role>> SaveAsync(Role role);
        Task<GenericResponse<Role>> UpdateAsync(Role role);
        Task<GenericResponse<Role>> DeleteAsync(int id);
         
    }
}