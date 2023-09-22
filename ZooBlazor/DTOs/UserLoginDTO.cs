using ZooCore.Models;

namespace ZooBlazor.DTOs
{
    public class UserLoginDTO
    {
        public string Token {  get; set; } 
        public string Message { get; set; } 
        public User User { get; set; }


    }
}
