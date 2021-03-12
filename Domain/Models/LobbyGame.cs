using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnityArenaApi.Domain.Models
{
    public class LobbyGame
    {
        public int Id{get;set;}
        public DateTime Date{get;set;}
        public int ModeId{get;set;}
        
        [ForeignKey("ModeId")]//возможна ошибка
        public Mode Mode{get;set;}
        public IList<PlayerLobbyGame> PlayerLobbyGames{get;set;} = new List<PlayerLobbyGame>();
    }
}