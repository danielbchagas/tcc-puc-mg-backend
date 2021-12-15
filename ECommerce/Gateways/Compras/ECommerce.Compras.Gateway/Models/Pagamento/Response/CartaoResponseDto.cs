namespace ECommerce.Compras.Gateway.Models.Pagamento.Response
{
    public class CartaoResponseDto
    {
        public CartaoResponseDto(bool aprovado, string mensagem)
        {
            Aprovado = aprovado;
            Mensagem = mensagem;
        }

        public bool Aprovado { get; private set; }
        public string Mensagem { get; private set; }
    }
}
