using System.Collections.Generic;

namespace UnityArenaApi.Domain.Resources
{
    public class PlayerResource
    {
        public int Id{get;set;}
        public string Login{get;set;}
        public string GameLogin{get;set;}
        public string Email{get;set;}
        public string Token{get;set;}
        public int PlayerInfoId{get;set;}
        public PlayerInfoResource PlayerInfo{get;set;}
        public IList<PlayerRoleResource> PlayerRoles{get;set;}
    }
}