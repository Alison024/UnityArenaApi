using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityArenaApi.Domain.IRepositories;
using UnityArenaApi.Domain.IServices;
using UnityArenaApi.Domain.Models;
using UnityArenaApi.Domain.Responses;
using UnityArenaApi.Helpers;
namespace UnityArenaApi.Services
{
    public class PlayerService:IPlayerService
    {
        private readonly IUnityOfWork unityOfWork;
        private readonly IPlayerRepository playerRepository;
        public PlayerService(IUnityOfWork unityOfWork,IPlayerRepository playerRepository){
            this.unityOfWork = unityOfWork;
            this.playerRepository = playerRepository;
        }

        public async Task<GenericResponse<Player>> DeleteAsync(int id)
        {
            var isExist = await playerRepository.FindByIdAsync(id);
            if(isExist == null)
                return new GenericResponse<Player>("Player doesn't exist!");
            try{
                playerRepository.Delete(isExist);
                await unityOfWork.CompleteAsync();
                return new GenericResponse<Player>(isExist);
            }
            catch(Exception ex){
                return new GenericResponse<Player>($"Error with deleting player: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            return await playerRepository.GetAllAsync();
        }

        public async Task<Player> GetById(int id)
        {
            return await playerRepository.FindByIdAsync(id);
        }

        public async Task<GenericResponse<Player>> SaveAsync(Player player)
        {
            try{
                await playerRepository.AddAsync(player);
                await unityOfWork.CompleteAsync();
                return new GenericResponse<Player>(player);
            }
            catch(Exception ex){
                return new GenericResponse<Player>($"Error while saving plaer. Message:{ex.Message}");
            }
        }

        public async Task<GenericResponse<Player>> UpdateAsync(Player player)
        {
            var isExist = await playerRepository.FindByIdAsync(player.Id);
            if (isExist == null)
                return new GenericResponse<Player>("Player not found!");
            
            try
            {
                player.Password = isExist.Password;
                player.PlayerInfoId = isExist.PlayerInfoId;
                playerRepository.Update(player);
                await unityOfWork.CompleteAsync();
                return new GenericResponse<Player>(player);
            }
            catch (Exception ex)
            {
                return new GenericResponse<Player>($"Error when updating player: {ex.Message}");
            }
        }
    }
}