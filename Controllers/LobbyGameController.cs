using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using UnityArenaApi.Extensions;
using UnityArenaApi.Domain.Models;
using UnityArenaApi.Domain.IServices;
using UnityArenaApi.Domain.Resources;
using Microsoft.AspNetCore.Authorization;

namespace UnityArenaApi.Controllers
{
    [ApiController]
    [Route("api/lobbyGames")]
    public class LobbyGameController:ControllerBase
    {
        private readonly ILobbyGameService lobbyGameService;
        private readonly IMapper mapper;
        public LobbyGameController(ILobbyGameService lobbyGameService,IMapper mapper){
            this.lobbyGameService = lobbyGameService;
            this.mapper = mapper;
        }
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<LobbyGameResource>> GetAllAsync(){
            var games = await lobbyGameService.GetAllAsync();
            var resource = mapper.Map<IEnumerable<LobbyGame>, IEnumerable<LobbyGameResource>>(games);
            return resource;
        }
        [Authorize(Roles="Admin,Player")]
        [HttpGet("getLobbyGameById/{id}")]
        public async Task<LobbyGameResource> GetUserById(int id){
            var users = await lobbyGameService.GetAllAsync();
            var user = users.SingleOrDefault(x=>x.Id==id);
            var resource = mapper.Map<LobbyGame,LobbyGameResource>(user);
            return resource;
        }
        [Authorize(Roles="Admin,Player")]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] LobbyGameResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var lobbyGame = mapper.Map<LobbyGameResource, LobbyGame>(resource);
            var result = await lobbyGameService.SaveAsync(lobbyGame);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            var userResource = mapper.Map<LobbyGame, LobbyGameResource>(result.internalValue);
            return Ok(userResource);
        }
        [Authorize(Roles="Admin")]
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] LobbyGameResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var lobbyGame = mapper.Map<LobbyGameResource, LobbyGame>(resource);
            var result = await lobbyGameService.UpdateAsync(lobbyGame);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            var lobbyGameResource = mapper.Map<LobbyGame, LobbyGameResource>(result.internalValue);
            return Ok(lobbyGameResource);
        }
        [Authorize(Roles="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await lobbyGameService.DeleteAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            var lobbyGameResource = mapper.Map<LobbyGame, LobbyGameResource>(result.internalValue);
            return Ok(lobbyGameResource);
        }
    }
}