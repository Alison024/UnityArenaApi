using System;
using System.Linq;
using System.Text;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using UnityArenaApi.Domain.Models;
namespace UnityArenaApi.Extensions
{
    public static class PlayerExtensions
    {
        public static void GenerateToken(this Player player, string secret, int expire){
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var claims = new List<Claim>();
            claims.AddRange(player.PlayerRoles.Select(x => new Claim(ClaimTypes.Role, x.Role.Name)));
            claims.Add(new Claim(ClaimTypes.Name, player.Login));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expire),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            player.Token = tokenHandler.WriteToken(token);
            player.Password = null;
        }
    }
}