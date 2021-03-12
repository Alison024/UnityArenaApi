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
    public class PlayerLobbyGameService:IPlayerLobbyGameService
    {
        private readonly IUnityOfWork unityOfWork;
        private readonly IPlayerLobbyGameRepository playerLobbyGameRepository;
        public PlayerLobbyGameService(IUnityOfWork unityOfWork,IPlayerLobbyGameRepository playerLobbyGameRepository){
            this.unityOfWork=unityOfWork;
            this.playerLobbyGameRepository = playerLobbyGameRepository;
        }

        public async Task<GenericResponse<PlayerLobbyGame>> DeleteAsync(PlayerLobbyGame playerLobbyGame)
        {
            var isExist = await playerLobbyGameRepository.
            FindByCompatibleKeyAsync(playerLobbyGame.PlayerId,playerLobbyGame.LobbyGameId);
            if(isExist == null)
                return new GenericResponse<PlayerLobbyGame>("PlayerLobbyGame doesn't exist!");

            try{
                playerLobbyGameRepository.Delete(isExist);
                await unityOfWork.CompleteAsync();
                return new GenericResponse<PlayerLobbyGame>(isExist);
            }
            catch(Exception ex){
                return new GenericResponse<PlayerLobbyGame>($"Error with deleting PlayerLobbyGame: {ex.Message}");
            }
        }

        public async Task<IEnumerable<PlayerLobbyGame>> GetAllAsync()
        {
            return await playerLobbyGameRepository.GetAllAsync();
        }

        public async Task<PlayerLobbyGame> GetByCompatibleKeyAsync(int playerId, int lobbyId)
        {
            return await playerLobbyGameRepository.FindByCompatibleKeyAsync(playerId,lobbyId);
        }

        public async Task<IEnumerable<PlayerLobbyGame>> GetByLobbyGameIdAsync(int lobbyGameId)
        {
            return await playerLobbyGameRepository.GetByLobbyGameId(lobbyGameId);
        }

        public async Task<IEnumerable<PlayerLobbyGame>> GetByPlayerIdAsync(int playerId)
        {
            return await playerLobbyGameRepository.GetByPlayerId(playerId);
        }

        public async Task<GenericResponse<PlayerLobbyGame>> SaveAsync(PlayerLobbyGame playerLobbyGame)
        {
            try{
                await playerLobbyGameRepository.AddAsync(playerLobbyGame);
                await unityOfWork.CompleteAsync();
                return new GenericResponse<PlayerLobbyGame>(playerLobbyGame);
            }
            catch(Exception ex){
                return new GenericResponse<PlayerLobbyGame>($"Error while saving playerLobbyGame. Message:{ex.Message}");
            }
        }

        /*public async Task<GenericResponse<PlayerLobbyGame>> UpdateAsync(PlayerLobbyGame playerLobbyGame)
        {
            var isExist = await playerLobbyGameRepository.
            FindByCompatibleKeyAsync(playerLobbyGame.PlayerId,playerLobbyGame.LobbyGameId);
            if (isExist == null)
                return new GenericResponse<PlayerLobbyGame>("PlayerLobbyGame not found!");
            
            try
            {
                playerLobbyGameRepository.Update(playerLobbyGame);
                await unityOfWork.CompleteAsync();
                return new GenericResponse<PlayerLobbyGame>(playerLobbyGame);
            }
            catch (Exception ex)
            {
                return new GenericResponse<PlayerLobbyGame>($"Error when updating PlayerLobbyGame: {ex.Message}");
            }
        }*/
    }
}