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
    public class PlayerInfoService: IPlayerInfoService
    {
        private readonly IUnityOfWork unityOfWork;
        private readonly IPlayerInfoRepository playerInfoRepository;

        public PlayerInfoService(IUnityOfWork unityOfWork,IPlayerInfoRepository playerInfoRepository){
            this.unityOfWork = unityOfWork;
            this.playerInfoRepository = playerInfoRepository;
        }

        public async Task<GenericResponse<PlayerInfo>> DeleteAsync(int id)
        {
            var isExist = await playerInfoRepository.FindByIdAsync(id);
            if(isExist == null)
                return new GenericResponse<PlayerInfo>("Info of player doesn't exist!");

            try{
                playerInfoRepository.Delete(isExist);
                await unityOfWork.CompleteAsync();
                return new GenericResponse<PlayerInfo>(isExist);
            }
            catch(Exception ex){
                return new GenericResponse<PlayerInfo>($"Error with deleting player info: {ex.Message}");
            }
        }

        public async Task<IEnumerable<PlayerInfo>> GetAllAsync()
        {
            return await playerInfoRepository.GetAllAsync();
        }

        public async Task<PlayerInfo> GetById(int id)
        {
            return await playerInfoRepository.FindByIdAsync(id);
        }

        public async Task<GenericResponse<PlayerInfo>> SaveAsync(PlayerInfo playerInfo)
        {
            try{
                await playerInfoRepository.AddAsync(playerInfo);
                await unityOfWork.CompleteAsync();
                return new GenericResponse<PlayerInfo>(playerInfo);
            }
            catch(Exception ex){
                return new GenericResponse<PlayerInfo>($"Error while saving player info. Message:{ex.Message}");
            }
        }

        public async Task<GenericResponse<PlayerInfo>> UpdateAsync(PlayerInfo playerInfo)
        {
            var existAvatarImage = await playerInfoRepository.FindByIdAsync(playerInfo.Id);
            if (existAvatarImage == null)
                return new GenericResponse<PlayerInfo>("Info of player not found!");
            
            try
            {
                playerInfoRepository.Update(playerInfo);
                await unityOfWork.CompleteAsync();
                return new GenericResponse<PlayerInfo>(playerInfo);
            }
            catch (Exception ex)
            {
                return new GenericResponse<PlayerInfo>($"Error when updating player info: {ex.Message}");
            }
        }
    }
}