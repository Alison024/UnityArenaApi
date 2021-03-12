using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityArenaApi.Domain.IRepositories;
using UnityArenaApi.Domain.IServices;
using UnityArenaApi.Domain.Models;
using UnityArenaApi.Domain.Responses;
namespace UnityArenaApi.Services
{
    public class PlayerRoleService:IPlayerRoleService
    {
        private readonly IUnityOfWork unityOfWork;
        private readonly IPlayerRoleRepository playerRoleRepository;

        public PlayerRoleService(IUnityOfWork unityOfWork,IPlayerRoleRepository playerRoleRepository){
            this.unityOfWork = unityOfWork;
            this.playerRoleRepository = playerRoleRepository;
        }
        public async Task<GenericResponse<PlayerRole>> DeleteAsync(PlayerRole playerRole)
        {
            var isExist = await playerRoleRepository.
            FindByCompatibleKeyAsync(playerRole.PlayerId,playerRole.RoleId);
            if(isExist == null)
                return new GenericResponse<PlayerRole>("PlayerRole doesn't exist!");

            try{
                playerRoleRepository.Delete(isExist);
                await unityOfWork.CompleteAsync();
                return new GenericResponse<PlayerRole>(isExist);
            }
            catch(Exception ex){
                return new GenericResponse<PlayerRole>($"Error with deleting PlayerRole: {ex.Message}");
            }
        }

        public async Task<IEnumerable<PlayerRole>> GetAllAsync()
        {
            return await playerRoleRepository.GetAllAsync();
        }

        public async Task<PlayerRole> GetByCompatibleKeyAsync(int playerId, int roleId)
        {
            return await playerRoleRepository.FindByCompatibleKeyAsync(playerId,roleId);
        }

        public async Task<IEnumerable<PlayerRole>> GetByPlayerIdAsync(int playerId)
        {
            return await playerRoleRepository.GetAllAsync();
        }

        public async Task<IEnumerable<PlayerRole>> GetByRoleIdAsync(int roleId)
        {
            return await playerRoleRepository.GetAllAsync();
        }

        public async Task<GenericResponse<PlayerRole>> SaveAsync(PlayerRole playerRole)
        {
            try{
                await playerRoleRepository.AddAsync(playerRole);
                await unityOfWork.CompleteAsync();
                return new GenericResponse<PlayerRole>(playerRole);
            }
            catch(Exception ex){
                return new GenericResponse<PlayerRole>($"Error while saving playerRole. Message:{ex.Message}");
            }
        }
    }
}