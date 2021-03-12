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
    [Route("api/playersInfo")]
    public class PlayerInfoController:ControllerBase
    {
        private readonly IPlayerInfoService playerInfoService;
        private readonly IMapper mapper;
        public PlayerInfoController(IPlayerInfoService playerInfoService,IMapper mapper){
            this.playerInfoService = playerInfoService;
            this.mapper = mapper;
        }

        //[Authorize(Roles="Admin, Player")]
        [HttpGet]
        public async Task<IEnumerable<PlayerInfoResource>> GetAllAsync(){
            var infos = await playerInfoService.GetAllAsync();
            var resource = mapper.Map<IEnumerable<PlayerInfo>, IEnumerable<PlayerInfoResource>>(infos);
            return resource;
        }
        //[Authorize(Roles="Admin, Player")]
        [HttpGet("getInfoById/{id}")]
        public async Task<PlayerInfoResource> GetUserById(int id){
            var infos = await playerInfoService.GetAllAsync();
            var info = infos.SingleOrDefault(x=>x.Id==id);
            var resource = mapper.Map<PlayerInfo,PlayerInfoResource>(info);
            return resource;
        }

        /*[HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PlayerInfoResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var info = mapper.Map<PlayerInfoResource, PlayerInfo>(resource);
            var result = await playerInfoService.SaveAsync(info);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            var infoResource = mapper.Map<PlayerInfo, PlayerInfoResource>(result.internalValue);
            return Ok(infoResource);
        }*/
        //[Authorize(Roles="Admin, Player")]
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] PlayerInfoResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var playerInfo = mapper.Map<PlayerInfoResource, PlayerInfo>(resource);
            var result = await playerInfoService.UpdateAsync(playerInfo);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            var playerInfoResource = mapper.Map<PlayerInfo, PlayerInfoResource>(result.internalValue);
            return Ok(playerInfoResource);
        }
        //[Authorize(Roles="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await playerInfoService.DeleteAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            var infoResource = mapper.Map<PlayerInfo, PlayerInfoResource>(result.internalValue);
            return Ok(infoResource);
        }
    }
}