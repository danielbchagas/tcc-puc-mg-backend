namespace ECommerce.Identity.Api.Models
{
    public class NovoUsuario
    {
        public string Documento { get; set; }
        public string Senha { get; set; }
        public string ConfirmacaoSenha { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}
