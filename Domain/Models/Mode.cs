using System.Collections.Generic;
namespace UnityArenaApi.Domain.Models
{
    public class Mode
    {
        public int Id{get;set;}
        public string ModeName{get;set;}
        public IList<LobbyGame> LobbyGames{get;set;} = new List<LobbyGame>();
    }
}