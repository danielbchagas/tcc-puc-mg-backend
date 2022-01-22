namespace ECommerce.Identidade.Api.Models
{
    public class SignUpUserDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
