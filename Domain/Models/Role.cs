using System.Collections.Generic;
namespace UnityArenaApi.Domain.Models
{
    public class Role
    {
        public int Id{get;set;}
        public string Name{get;set;}
        public IList<PlayerRole> PlayerRoles{get;set;} = new List<PlayerRole>();
    }
}