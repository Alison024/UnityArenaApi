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
    [Route("api/playerRoles")]
    public class PlayerRoleController:ControllerBase
    {
        private readonly IPlayerRoleService playerRoleService;
        private readonly IMapper mapper;
        public PlayerRoleController(IPlayerRoleService playerRoleService,IMapper mapper){
            this.playerRoleService = playerRoleService;
            this.mapper = mapper;
        }
        //[Authorize(Roles="Admin,Player")]
        [HttpGet]
        public async Task<IEnumerable<PlayerRoleResource>> GetAllAsync(){
            var roles = await playerRoleService.GetAllAsync();
            var resource = mapper.Map<IEnumerable<PlayerRole>, IEnumerable<PlayerRoleResource>>(roles);
            return resource;
        }
        //[Authorize(Roles="Admin,Player")]
        [HttpGet("getPlayerRoleByPlayerId/{id}")]
        public async Task<PlayerRoleResource> GetPlayerRoleByPlayerId(int id){
            var roles = await playerRoleService.GetAllAsync();
            var role = roles.SingleOrDefault(x=>x.PlayerId==id);
            var resource = mapper.Map<PlayerRole,PlayerRoleResource>(role);
            return resource;
        }
        //[Authorize(Roles="Admin")]
        [HttpGet("getInfoByGameId/{id}")]
        public async Task<PlayerRoleResource> GetPlayerRoleByRoleId(int id){
            var roles = await playerRoleService.GetAllAsync();
            var role = roles.SingleOrDefault(x=>x.RoleId==id);
            var resource = mapper.Map<PlayerRole,PlayerRoleResource>(role);
            return resource;
        }
        //[Authorize(Roles="Admin")]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PlayerRoleResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var playerRole = mapper.Map<PlayerRoleResource, PlayerRole>(resource);
            var result = await playerRoleService.SaveAsync(playerRole);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            var playerRoleResource = mapper.Map<PlayerRole, PlayerRoleResource>(result.internalValue);
            return Ok(playerRoleResource);
        }
        //[Authorize(Roles="Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] PlayerRoleResource resource)
        {

            var isExist = await playerRoleService.GetByCompatibleKeyAsync(resource.PlayerId,resource.RoleId);
            if (isExist == null)
                return BadRequest("PlayerLobbyGame doesn't exist.");
            var result = await playerRoleService.DeleteAsync(isExist);
            var playerRoleResource = mapper.Map<PlayerRole, PlayerRoleResource>(result.internalValue);
            return Ok(playerRoleResource);
        }
    }
}