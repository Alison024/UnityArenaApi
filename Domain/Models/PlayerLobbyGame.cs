using System.ComponentModel.DataAnnotations.Schema;
namespace UnityArenaApi.Domain.Models
{
    public class PlayerLobbyGame
    {
        public int PlayerId{get;set;}
        //[ForeignKey("PlayerId")]
        public Player Player{get;set;}
        public int LobbyGameId{get;set;}
        //[ForeignKey("LobbyGameId")]
        public LobbyGame LobbyGame{get;set;}
    }
}