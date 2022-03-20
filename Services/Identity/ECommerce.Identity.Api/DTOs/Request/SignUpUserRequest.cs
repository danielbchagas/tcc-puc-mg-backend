using System.ComponentModel.DataAnnotations;

namespace ECommerce.Identity.Api.DTOs.Request
{
    public class SignUpUserRequest
    {
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "O campo sobrenome é obrigatório.")]
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
        [Required(ErrorMessage = "O campo email é obrigatório.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "O campo confirmação de senha é obrigatório.")]
        [Compare(nameof(Password), ErrorMessage = "As senhas informadas são diferentes.")]
        public string PasswordConfirmation { get; set; }
    }
}
