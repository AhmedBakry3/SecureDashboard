using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels.AccountViewModel
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password is Required")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is Required")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } 
    }
}
