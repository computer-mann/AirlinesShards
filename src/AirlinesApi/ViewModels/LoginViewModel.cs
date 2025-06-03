using System.ComponentModel.DataAnnotations;

namespace AirlinesApi.ViewModels
{
    public class LoginViewModel
    {
       // [EmailAddress]
        public string Username { get; set; }
       // [MinLength(6)]
        public string Password { get; set; }
    }
}
