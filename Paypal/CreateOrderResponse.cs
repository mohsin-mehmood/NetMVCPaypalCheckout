using System.Text.Json.Serialization;

namespace PaypalDemo.Paypal
{

    public class CreateOrderResponse
    {
        [JsonPropertyName("id")]
        public string OrderId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("links")]
        public Link[] Links { get; set; }

        [JsonPropertyName("create_time")]
        public string CreateDateTime { get; set; }

        [JsonPropertyName("update_time")]
        public string UpdateDateTime { get; set; }
    }

    public class Link
    {
        public string href { get; set; }
        public string rel { get; set; }
        public string method { get; set; }
    }

}
