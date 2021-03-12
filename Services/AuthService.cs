using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UnityArenaApi.Domain.IRepositories;
using UnityArenaApi.Domain.IServices;
using UnityArenaApi.Domain.Models;
using UnityArenaApi.Extensions;
using UnityArenaApi.Helpers;

namespace UnityArenaApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppSettings appSettings;
        private readonly IPlayerRepository playerRepository;
        public AuthService(IOptions<AppSettings> appSettings,IPlayerRepository playerRepository){
            this.appSettings = appSettings.Value;
            this.playerRepository = playerRepository;
        }
        public async Task<Player> Authenticate(string login, string password)
        {
            password = HelperMD5.GenerateMD5Hash(password);
            var player = (await playerRepository.GetAllAsync()).SingleOrDefault(x => x.Login == login && x.Password == password);  
            if (player == null)
                return null;

            player.GenerateToken(appSettings.Secret, appSettings.ExpiresMinutes);
            return player;
        }
    }
}