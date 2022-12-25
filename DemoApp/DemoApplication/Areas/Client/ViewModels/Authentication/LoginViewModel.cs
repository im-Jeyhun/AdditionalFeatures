using System.ComponentModel.DataAnnotations;

namespace DemoApplication.Areas.Client.ViewModels.Authentication
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }



    }
}
