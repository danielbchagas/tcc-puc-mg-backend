namespace ECommerce.Ordering.Gateway.DTOs.Response
{
    public class PaymentResponse
    {
        public decimal Value { get; set; }
        public bool Approved { get; set; }
        public string Message { get; set; }
    }
}
