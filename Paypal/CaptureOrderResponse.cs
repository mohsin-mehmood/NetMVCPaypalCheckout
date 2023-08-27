using System;

namespace PaypalDemo.Paypal
{

    public class CaptureOrderResponse
    {
        public string id { get; set; }
        public string status { get; set; }
        public Payment_Source payment_source { get; set; }
        public Purchase_Units[] purchase_units { get; set; }
        public Payer payer { get; set; }
        public Link[] links { get; set; }
    }

    public class Payment_Source
    {
        public Paypal paypal { get; set; }
    }

    public class Paypal
    {
        public string email_address { get; set; }
        public string account_id { get; set; }
        public string account_status { get; set; }
        public Name name { get; set; }
        public Address address { get; set; }
    }

    public class Name
    {
        public string given_name { get; set; }
        public string surname { get; set; }
    }


    public class Payer
    {
        public Name name { get; set; }
        public string email_address { get; set; }
        public string payer_id { get; set; }
        public Address address { get; set; }
    }

    public class Purchase_Units
    {
        public string reference_id { get; set; }
        public Shipping shipping { get; set; }
        public Payments payments { get; set; }
    }

    public class Shipping
    {
        public FullName name { get; set; }
        public Address address { get; set; }
    }

    public class FullName
    {
        public string full_name { get; set; }
    }

    public class Address
    {
        public string address_line_1 { get; set; }
        public string admin_area_2 { get; set; }
        public string admin_area_1 { get; set; }
        public string postal_code { get; set; }
        public string country_code { get; set; }
    }

    public class Payments
    {
        public Capture[] captures { get; set; }
    }

    public class Capture
    {
        public string id { get; set; }
        public string status { get; set; }
        public Status_Details status_details { get; set; }
        public Amount amount { get; set; }
        public bool final_capture { get; set; }
        public Seller_Protection seller_protection { get; set; }
        public string invoice_id { get; set; }
        public Link[] links { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
    }

    public class Status_Details
    {
        public string reason { get; set; }
    }

    public class Amount
    {
        public string currency_code { get; set; }
        public string value { get; set; }
    }

    public class Seller_Protection
    {
        public string status { get; set; }
        public string[] dispute_categories { get; set; }
    }

}
