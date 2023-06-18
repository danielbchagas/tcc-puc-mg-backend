using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Identity.Api.Models.Request
{
    public class SignUpUserRequest
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "O campo sobrenome é obrigatório.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "O campo documento é obrigatório.")]
        public string Document { get; set; }
        [Required(ErrorMessage = "O campo telefone é obrigatório.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "O campo email é obrigatório.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "O campo confirmação de senha é obrigatório.")]
        [Compare(nameof(Password), ErrorMessage = "As senhas informadas são diferentes.")]
        public string PasswordConfirmation { get; set; }
        [Required(ErrorMessage = "O campo código postal é obrigatório.")]
        public string ZipCode { get; set; }
    }
}
