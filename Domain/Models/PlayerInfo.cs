namespace UnityArenaApi.Domain.Models
{
    public class PlayerInfo
    {
        public int Id{get;set;}
        public int PassedGames{get;set;}
        public int MaxKills{get;set;}
        public int MaxDamage{get;set;}
        //public int PlayerId{get;set;}
        public Player Player{get;set;}
    }
}