using System.Text.Json.Serialization;
using WorkBook.util;

namespace WorkBook.DTOs
{

    

    public class CustomerModelReq
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string address_1 { get; set; }
        public string? address_2 { get; set; }
        public string? address_3 { get; set; }

        [field: JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly date_of_birth { get; set; }
        public string home_phone { get; set; }


    }

    public class EditCustomerReq
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address_1 { get; set; }
        public string? address_2 { get; set; }
        public string? address_3 { get; set; }

        [field: JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly date_of_birth { get; set; }
    }

    public class CustomerProfileResp
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address_1 { get; set; }
        public string? address_2 { get; set; }
        public string? address_3 { get; set; }

        [field: JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly date_of_birth { get; set; }
    }

    public record RateReq(int value, string description);

}
