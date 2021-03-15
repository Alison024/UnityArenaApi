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
    [Route("api/players")]
    public class PlayerController:ControllerBase
    {
        private readonly IPlayerService playerService;
        private readonly IMapper mapper;
        public PlayerController(IPlayerService playerService,IMapper mapper)
        {
            this.playerService =playerService;
            this.mapper = mapper;
        }
        [Authorize(Roles="Admin")]
        [HttpGet]
        public async Task<IEnumerable<PlayerResource>> GetAllAsync(){
            var player = await playerService.GetAllAsync();
            var resource = mapper.Map<IEnumerable<Player>, IEnumerable<PlayerResource>>(player);
            return resource;
        }
        [Authorize(Roles="Admin,Player")]
        [HttpGet("getPLayerById/{id}")]
        public async Task<PlayerResource> GetUserById(int id){
            var players = await playerService.GetAllAsync();
            var player = players.SingleOrDefault(x=>x.Id==id);
            var resource = mapper.Map<Player,PlayerResource>(player);
            return resource;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SavePlayerResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var player = mapper.Map<SavePlayerResource, Player>(resource);
            player.Password = HelperMD5.GenerateMD5Hash(player.Password);
            player.PlayerInfo = new PlayerInfo{MaxDamage=0,MaxKills=0,PassedGames=0};
            var result = await playerService.SaveAsync(player);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            var customerResource = mapper.Map<Player, PlayerResource>(result.internalValue);
            return Ok(customerResource);
        }
        //обновлять можно только логин, ник, почту 
        [Authorize(Roles="Admin,Player")]
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] PlayerResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var player = mapper.Map<PlayerResource, Player>(resource);
            var result = await playerService.UpdateAsync(player);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            var userResource = mapper.Map<Player, PlayerResource>(result.internalValue);
            return Ok(userResource);
        }
        [Authorize(Roles="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await playerService.DeleteAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            var playerResource = mapper.Map<Player, PlayerResource>(result.internalValue);
            return Ok(playerResource);
        }

    }
}