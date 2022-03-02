using System.ComponentModel.DataAnnotations;

namespace ECommerce.Identity.Api.Models
{
    public class SignUpUserDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        public string PasswordConfirmation { get; set; }
    }
}
