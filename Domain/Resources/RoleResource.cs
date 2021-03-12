using System.Collections.Generic;

namespace UnityArenaApi.Domain.Resources
{
    public class RoleResource
    {
        public int Id{get;set;}
        public string Name{get;set;}
        public IList<PlayerRoleResource> PlayerRoles{get;set;}
    }
}