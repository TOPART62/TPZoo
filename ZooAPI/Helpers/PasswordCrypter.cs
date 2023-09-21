using System.Text;

namespace ZooAPI.Helpers
{
    public class PasswordCrypter
    {
        public static string Encrypt(string password, string key) 
        {
            if (string.IsNullOrEmpty(password)) return "";
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password + key));
        }

        //private static string DecryptPassword(string? cryptedString, string key)
        //{
        //    if (string.IsNullOrEmpty(cryptedString)) return "";
        //    string decriptedString = Encoding.UTF8.GetString(Convert.FromBase64String(cryptedString));
        //    return decriptedString.Substring(0, decriptedString.Length - key.Length);
        //}
    }
}
