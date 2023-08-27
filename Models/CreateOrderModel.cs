using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PaypalDemo.Models
{
    public static class Intents
    {
        public static string CAPTURE = "CAPTURE";
        public static string AUTHORIZE = "AUTHORIZE";
    }

    public class CreateOrderModel
    {
        private readonly string _intent;
        public CreateOrderModel(string intent)
        {
            _intent = intent;
        }

        public class AmountModel
        {
            /// <summary>
            /// The three-character ISO-4217 currency code that identifies the currency.
            /// </summary>
            [JsonPropertyName("currency_code")]
            public string CurrencyCode { get; set; }

            /// <summary>
            /// The value, which might be: An integer for currencies like JPY that are not typically fractional. A decimal fraction for currencies like TND that are subdivided into thousandths. For the required number of decimal places for a currency code, see Currency Codes.
            /// </summary>
            [JsonPropertyName("value")]
            public string Value { get; set; }
        }

        public class PurchaseUnitsModel
        {

            private readonly AmountModel _amount;
            public PurchaseUnitsModel(AmountModel amount)
            {
                _amount = amount;
            }
            /// <summary>
            /// The API caller-provided external ID for the purchase unit. Required for multiple purchase units when you must update the order through PATCH. If you omit this value and the order contains only one purchase unit, PayPal sets this value to default.
            /// </summary>
            [JsonPropertyName("reference_id")]
            public string ReferenceId { get; set; }

            /// <summary>
            /// The purchase description. The maximum length of the character is dependent on the type of characters used. The character length is specified assuming a US ASCII character. Depending on type of character; (e.g. accented character, Japanese characters) the number of characters that that can be specified as input might not equal the permissible max length.
            /// </summary>
            [JsonPropertyName("description")]
            public string Description { get; set; }

            /// <summary>
            /// The API caller-provided external ID. Used to reconcile client transactions with PayPal transactions. Appears in transaction and settlement reports but is not visible to the payer.
            /// </summary>
            [JsonPropertyName("custom_id")]
            public string CustomId { get; set; }

            /// <summary>
            /// The API caller-provided external invoice number for this order. Appears in both the payer's transaction history and the emails that the payer receives.
            /// </summary>
            [JsonPropertyName("invoice_id")]
            public string InvoiceId { get; set; }


            [JsonPropertyName("amount")]
            public AmountModel Amount { get => _amount; }
        }

        [JsonPropertyName("intent")]
        public string Intent { get => _intent; }

        [JsonPropertyName("purchase_units")]
        public IEnumerable<PurchaseUnitsModel> PurchaseUnits { get; set; }


    }
}
