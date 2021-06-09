using System.ComponentModel.DataAnnotations;

namespace OAWA.Data.Dtos
{
    public class UserForLoginDto
    {
        [Required]
        public string Email {get;set;}
        
        [Required]
        public string Password {get;set;}
    }

    public class TokenRefreshDto
    {
        public string Token {get;set;}
        
        public string RefreshToken {get;set;}
        public string FcmToken { get; set; }
    }
}