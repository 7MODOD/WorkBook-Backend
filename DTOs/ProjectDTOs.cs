using System.Text.Json.Serialization;
using WorkBook.Models;
using WorkBook.util;

namespace WorkBook.DTOs
{
    public class EditProjectReq
    {
        public string title { get; set; }
        public string description { get; set; }

        [field: JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly start_date { get; set; }
        
        [field: JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly end_date { get; set; }

        public int duration_days { get; set; }
        public int price { get; set; }
        public int profession_id { get; set; }
        public string status { get; set; }
        public string address { get; set; }

    }

    public class CreateProjectReq
    {
        public string title { get; set; }
        public string description { get; set; }

        [field: JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly start_date { get; set; }
        [field: JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly end_date { get; set; }

        public int duration_days { get; set; }
        public int price { get; set; }
        public int profession_id { get; set; }
        public string status { get; set; }
        public string address { get; set; }
        public int worker_id { get; set; }
    }

    public record ProjectResp(int id, string title, string description, DateOnly start_date, DateOnly end_date,
                                int duration_days, int price, int profession_id, string status, string address);




}
