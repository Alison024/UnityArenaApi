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
    public class LobbyGameService : ILobbyGameService
    {
        private readonly IUnityOfWork unityOfWork;
        private readonly ILobbyGameRepository lobbyGameRepository;

        public LobbyGameService(IUnityOfWork unityOfWork,ILobbyGameRepository lobbyGameRepository)
        {
            this.unityOfWork = unityOfWork;
            this.lobbyGameRepository = lobbyGameRepository;
        }
        public async Task<GenericResponse<LobbyGame>> DeleteAsync(int id)
        {
            var isExist = await lobbyGameRepository.FindByIdAsync(id);
            if(isExist == null)
                return new GenericResponse<LobbyGame>("Lobby Game doesn't exist!");

            try{
                lobbyGameRepository.Delete(isExist);
                await unityOfWork.CompleteAsync();
                return new GenericResponse<LobbyGame>(isExist);
            }
            catch(Exception ex){
                return new GenericResponse<LobbyGame>($"Error with deleting lobby game: {ex.Message}");
            }
        }

        public async Task<IEnumerable<LobbyGame>> GetAllAsync()
        {
            return await lobbyGameRepository.GetAllAsync();
        }

        public async Task<LobbyGame> GetById(int id)
        {
            return await lobbyGameRepository.FindByIdAsync(id);
        }

        public async Task<GenericResponse<LobbyGame>> SaveAsync(LobbyGame lobbyGame)
        {
            try{
                await lobbyGameRepository.AddAsync(lobbyGame);
                await unityOfWork.CompleteAsync();
                return new GenericResponse<LobbyGame>(lobbyGame);
            }
            catch(Exception ex){
                return new GenericResponse<LobbyGame>($"Error while saving lobby game. Message:{ex.Message}");
            }
        }

        public async Task<GenericResponse<LobbyGame>> UpdateAsync(LobbyGame lobbyGame)
        {
            var existLobbyGame = await lobbyGameRepository.FindByIdAsync(lobbyGame.Id);
            if (existLobbyGame == null)
                return new GenericResponse<LobbyGame>("Lobby game not found!");
            
            try
            {
                lobbyGameRepository.Update(lobbyGame);
                await unityOfWork.CompleteAsync();
                return new GenericResponse<LobbyGame>(lobbyGame);
            }
            catch (Exception ex)
            {
                return new GenericResponse<LobbyGame>($"Error when updating lobby game: {ex.Message}");
            }
        }
    }
}