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
    [Route("api/roles")]
    public class RoleController:ControllerBase
    {
        private readonly IRoleService roleService;
        private readonly IMapper mapper;
        public RoleController(IRoleService roleService,IMapper mapper){
            this.roleService = roleService;
            this.mapper = mapper;
        }

        [Authorize(Roles="Admin,Player")]
        [HttpGet]
        public async Task<IEnumerable<RoleResource>> GetAllAsync(){
            var modes = await roleService.GetAllAsync();
            var resource = mapper.Map<IEnumerable<Role>, IEnumerable<RoleResource>>(modes);
            return resource;
        }
        [Authorize(Roles="Admin,Player")]
        [HttpGet("getRoleById/{id}")]
        public async Task<RoleResource> GetUserById(int id){
            var users = await roleService.GetAllAsync();
            var user = users.SingleOrDefault(x=>x.Id==id);
            var resource = mapper.Map<Role,RoleResource>(user);
            return resource;
        }
        [Authorize(Roles="Admin")]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RoleResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var role = mapper.Map<RoleResource, Role>(resource);
            var result = await roleService.SaveAsync(role);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            var roleResource = mapper.Map<Role, RoleResource>(result.internalValue);
            return Ok(roleResource);
        }
        [Authorize(Roles="Admin")]
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] RoleResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var role = mapper.Map<RoleResource, Role>(resource);
            var result = await roleService.UpdateAsync(role);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            var roleResource = mapper.Map<Role, RoleResource>(result.internalValue);
            return Ok(roleResource);
        }
        [Authorize(Roles="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await roleService.DeleteAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            var roleResource = mapper.Map<Role, RoleResource>(result.internalValue);
            return Ok(roleResource);
        }
    }
}