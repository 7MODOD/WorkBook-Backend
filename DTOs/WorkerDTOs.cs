using System.Text.Json.Serialization;
using WorkBook.util;

namespace WorkBook.DTOs
{
    public record SignInResp ( string token, string id );
    public record SignInReq(string email, string password);


    public class WorkersModelReq
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string address_1 { get; set; }
        public string? address_2 { get; set; }
        public string? address_3 { get; set; }
        
        [field:JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly date_of_birth { get; set; }
        public List<int> profession_ids { get; set; }


    }
    
    public class EditWorkerReq
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address_1 { get; set; }
        public string? address_2 { get; set; }
        public string? address_3 { get; set; }

        [field: JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly date_of_birth { get; set; }
        public List<int> profession_ids { get; set; }
    }

    public class WorkerProfileResp
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address_1 { get; set; }
        public string? address_2 { get; set; }
        public string? address_3 { get; set; }

        [field: JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly date_of_birth { get; set; }
        public List<int> profession_ids { get; set; }
    }


    



}
