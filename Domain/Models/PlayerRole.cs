using System.ComponentModel.DataAnnotations.Schema;
namespace UnityArenaApi.Domain.Models
{
    public class PlayerRole
    {
        public int PlayerId{get;set;}

        //[ForeignKey("PlayerId")]
        public Player Player{get;set;}
        public int RoleId{get;set;}
        
        //[ForeignKey("RoleId")]
        public Role Role{get;set;}
    }
}