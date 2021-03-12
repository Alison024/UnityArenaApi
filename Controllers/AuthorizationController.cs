using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using UnityArenaApi.Extensions;
using UnityArenaApi.Domain.Models;
using UnityArenaApi.Domain.IServices;
using UnityArenaApi.Domain.Resources;
using UnityArenaApi.Domain.Responses;
using Microsoft.AspNetCore.Authorization;

namespace UnityArenaApi.Controllers
{
    [ApiController]
    [Route("api/authorization")]
    public class AuthorizationController:ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IMapper mapper;
        public AuthorizationController(IAuthService authService, IMapper mapper){
            this.authService = authService;
            this.mapper = mapper;
        }
        
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthResource authResource){
            var player = await authService.Authenticate(authResource.Login,authResource.Password);
            var result = mapper.Map<Player,PlayerResource>(player);
            
            var response = new GenericResponse<PlayerResource>(result != null,result != null ? "" : "Incorrect login or password",result);
            return Ok(response);
        }
    }
}