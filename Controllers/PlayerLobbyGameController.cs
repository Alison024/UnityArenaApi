using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using UnityArenaApi.Helpers;
using UnityArenaApi.Extensions;
using UnityArenaApi.Domain.Models;
using UnityArenaApi.Domain.IServices;
using UnityArenaApi.Domain.Resources;
using Microsoft.AspNetCore.Authorization;

namespace UnityArenaApi.Controllers
{
    [ApiController]
    [Route("api/playerLobbyGames")]
    public class PlayerLobbyGameController:ControllerBase
    {
        private readonly IPlayerLobbyGameService playerLobbyGameService;
        private readonly IMapper mapper;
        public PlayerLobbyGameController(IPlayerLobbyGameService playerLobbyGameService,IMapper mapper){
            this.playerLobbyGameService = playerLobbyGameService;
            this.mapper = mapper;
        }
        //[Authorize(Roles="Admin, Player")]
        [HttpGet]
        public async Task<IEnumerable<PlayerLobbyGameResource>> GetAllAsync(){
            var infos = await playerLobbyGameService.GetAllAsync();
            var resource = mapper.Map<IEnumerable<PlayerLobbyGame>, IEnumerable<PlayerLobbyGameResource>>(infos);
            return resource;
        }
        //[Authorize(Roles="Admin,Player")]
        [HttpGet("getPlayerGameByPlayerId/{id}")]
        public async Task<PlayerLobbyGameResource> GetPlayerLobbyGameByPlayerId(int id){
            var infos = await playerLobbyGameService.GetAllAsync();
            var info = infos.SingleOrDefault(x=>x.PlayerId==id);
            var resource = mapper.Map<PlayerLobbyGame,PlayerLobbyGameResource>(info);
            return resource;
        }
        [Authorize(Roles="Admin,Player")]
        [HttpGet("getPlayerGameByGameId/{id}")]
        public async Task<PlayerLobbyGameResource> GetPlayerLobbyGameByLobbyGameId(int id){
            var infos = await playerLobbyGameService.GetAllAsync();
            var info = infos.SingleOrDefault(x=>x.LobbyGameId==id);
            var resource = mapper.Map<PlayerLobbyGame,PlayerLobbyGameResource>(info);
            return resource;
        }
        [Authorize(Roles="Admin,Player")]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PlayerLobbyGameResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var playerGame = mapper.Map<PlayerLobbyGameResource, PlayerLobbyGame>(resource);
            var result = await playerLobbyGameService.SaveAsync(playerGame);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            var infoResource = mapper.Map<PlayerLobbyGame, PlayerLobbyGameResource>(result.internalValue);
            return Ok(infoResource);
        }
        [Authorize(Roles="Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] PlayerLobbyGameResource resource)
        {

            var isExsist = await playerLobbyGameService.GetByCompatibleKeyAsync(resource.PlayerId,resource.LobbyGameId);
            if (isExsist == null)
                return BadRequest("PlayerLobbyGame doesn't exist.");
            var result = await playerLobbyGameService.DeleteAsync(isExsist);
            var playerGamesResource = mapper.Map<PlayerLobbyGame, PlayerLobbyGameResource>(result.internalValue);
            return Ok(playerGamesResource);
        }
    }
}