using System;
using AutoMapper;
using UnityArenaApi.Domain.Models;
using UnityArenaApi.Domain.Resources;


namespace UnityArenaApi.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Mode,ModeResource>();
            CreateMap<ModeResource,Mode>();

            CreateMap<Role,RoleResource>();
            CreateMap<RoleResource,Role>();

            CreateMap<PlayerInfo,PlayerInfoResource>();
            CreateMap<PlayerInfoResource,PlayerInfo>();

            CreateMap<LobbyGame,LobbyGameResource>();
            CreateMap<LobbyGameResource,LobbyGame>().ForMember(x=>x.Date,y=>y.MapFrom(x=>Convert.ToDateTime(x.Date)));

            CreateMap<Player,PlayerResource>();
            CreateMap<PlayerResource,Player>();
            CreateMap<SavePlayerResource,Player>();

            CreateMap<PlayerLobbyGame,PlayerLobbyGameResource>();
            CreateMap<PlayerLobbyGameResource,PlayerLobbyGame>();

            CreateMap<PlayerRole,PlayerRoleResource>();
            CreateMap<PlayerRoleResource,PlayerRole>();
        }
    }
}