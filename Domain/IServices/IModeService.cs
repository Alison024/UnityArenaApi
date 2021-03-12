using System.Threading.Tasks;
using System.Collections.Generic;
using UnityArenaApi.Domain.Models;
using UnityArenaApi.Domain.Responses;
namespace UnityArenaApi.Domain.IServices
{
    public interface IModeService
    {
        Task<IEnumerable<Mode>> GetAllAsync();
        Task<Mode> GetById(int id);
        Task<GenericResponse<Mode>> SaveAsync(Mode mode);
        Task<GenericResponse<Mode>> UpdateAsync(Mode mode);
        Task<GenericResponse<Mode>> DeleteAsync(int id);
    }
}