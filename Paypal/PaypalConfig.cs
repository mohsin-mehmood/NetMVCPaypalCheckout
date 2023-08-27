namespace PaypalDemo.Paypal
{
    public enum PaypalMode
    {
        Sandbox,
        Production
    }

    public class ApiBaseUrlModel
    {
        public string Sandbox { get; set; }
        public string Production { get; set; }
    }


    public class PaypalConfig
    {
        public ApiBaseUrlModel ApiBaseUrl { get; set; }

        public string ClientID { get; set; }

        public string SecretKey { get; set; }

        public PaypalMode Mode { get; set; }

        public string ApiUrl
        {
            get
            {
                var apiBaseUrl = this.ApiBaseUrl.Production;

                if (this.Mode == PaypalMode.Sandbox)
                {
                    apiBaseUrl = this.ApiBaseUrl.Sandbox;
                }
                return apiBaseUrl;
            }
        }

    }
}
