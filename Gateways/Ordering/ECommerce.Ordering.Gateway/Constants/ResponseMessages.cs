namespace ECommerce.Ordering.Gateway.Constants
{
    public class ResponseMessages
    {
        public const string InconsistentIdentifiers = "Identificadores inconsistentes.";
        public const string InvalidOperation = "Operação inválida.";
        public const string OutOfStock = "Quantidade indisponível em estoque.";
        public const string CustomerNotFound = "Cliente não encontrado.";
        public const string BasketNotFound = "Carrinho não encontrado.";
        public const string CustomerInformationsNotFound = "Verifique seus dados cadastrais.";
        public const string CreateOrderFail = "Falha ao criar o pedido.";
        public const string PaymentRefused = "Saldo insuficiente.";
        public const string PaymentAccepted = "Pagamento aprovado.";
        public const string PaymentIsProcessing = "Pagamento em processamento.";
        public const string OrderNotFound = "Pedido não encontrado.";
    }
}
