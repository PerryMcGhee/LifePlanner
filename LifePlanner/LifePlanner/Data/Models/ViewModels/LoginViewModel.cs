using System.ComponentModel.DataAnnotations;

namespace LifePlanner.Data.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide User Name")]
        public string? UserName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide Password")]
        public string? Password { get; set; }

    }
}
