using System.Security.Cryptography;
using System.Text;
namespace UnityArenaApi.Helpers
{
    public class HelperMD5
    {
        public static string GenerateMD5Hash(string password)
        {
            byte[] hash = Encoding.ASCII.GetBytes(password);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashenc = md5.ComputeHash(hash);
            string result = "";
            foreach (var b in hashenc)
            {
            result += b.ToString("x2");
            }
            return result;
        }
    }
}