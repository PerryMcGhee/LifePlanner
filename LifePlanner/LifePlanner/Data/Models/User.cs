using System.ComponentModel.DataAnnotations;

namespace LifePlanner.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [RegularExpression(@"^(?=.*[A-Z]).+$", ErrorMessage = "Password must contain at least one capital letter")]
        public string Password { get; set; }
        public string Token { get; set; }
        public string UserRole { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
