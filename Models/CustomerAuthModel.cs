
using SQLite;

namespace WorkBook.Models
{
    public class CustomerAuthModel:BaseModel
    {
        
        public string Token { get; set; }
        public int CustomerId { get; set; }
        public CustomerModel Customer { get; set; }

    }
}
