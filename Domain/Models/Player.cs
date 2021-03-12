using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace UnityArenaApi.Domain.Models
{
    public class Player
    {
        public int Id{get;set;}
        public string Login{get;set;}
        public string GameLogin{get;set;}
        public string Email{get;set;}
        public string Password{get;set;}
        public string Token{get;set;}
        public IList<PlayerLobbyGame> PlayerLobbyGames{get;set;} = new List<PlayerLobbyGame>();
        public IList<PlayerRole> PlayerRoles{get;set;} = new List<PlayerRole>();
        public int PlayerInfoId{get;set;}
        
        [ForeignKey("PlayerInfoId")]
        public PlayerInfo PlayerInfo{get;set;}
    }
}