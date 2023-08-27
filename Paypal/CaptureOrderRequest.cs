using System.Text.Json.Serialization;

namespace PaypalDemo.Paypal
{
    public class CaptureOrderRequest
    {
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; }

        [JsonPropertyName("payerID")]
        public string PayerID { get; set; }

        [JsonPropertyName("paymentID")]
        public string PaymentID { get; set; }

        [JsonPropertyName("paymentSource")]
        public string PaymentSource { get; set; }
    }

}
