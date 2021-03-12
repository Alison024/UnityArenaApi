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
    [Route("api/modes")]
    public class ModeController:ControllerBase
    {
        private readonly IModeService modeService;
        private readonly IMapper mapper;
        public ModeController(IModeService lobbyGameService,IMapper mapper){
            this.modeService = lobbyGameService;
            this.mapper = mapper;
        }
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<ModeResource>> GetAllAsync(){
            var modes = await modeService.GetAllAsync();
            var resource = mapper.Map<IEnumerable<Mode>, IEnumerable<ModeResource>>(modes);
            return resource;
        }
        //[Authorize]
        [HttpGet("getModeById/{id}")]
        public async Task<ModeResource> GetUserById(int id){
            var users = await modeService.GetAllAsync();
            var user = users.SingleOrDefault(x=>x.Id==id);
            var resource = mapper.Map<Mode,ModeResource>(user);
            return resource;
        }
        //[Authorize(Roles="Admin")]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ModeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var mode = mapper.Map<ModeResource, Mode>(resource);
            var result = await modeService.SaveAsync(mode);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            var modeResource = mapper.Map<Mode, ModeResource>(result.internalValue);
            return Ok(modeResource);
        }
        //[Authorize(Roles="Admin")]
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] ModeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var mode = mapper.Map<ModeResource, Mode>(resource);
            var result = await modeService.UpdateAsync(mode);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            var modeResource = mapper.Map<Mode, ModeResource>(result.internalValue);
            return Ok(modeResource);
        }
        //[Authorize(Roles="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await modeService.DeleteAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            var modeResource = mapper.Map<Mode, ModeResource>(result.internalValue);
            return Ok(modeResource);
        }
    }
}