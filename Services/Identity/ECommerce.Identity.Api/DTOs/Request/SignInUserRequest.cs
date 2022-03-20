using System.ComponentModel.DataAnnotations;

namespace ECommerce.Identity.Api.DTOs.Request
{
    public class SignInUserRequest
    {
        [Required(ErrorMessage = "O campo email é obrigatório.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        public string Password { get; set; }
    }
}
