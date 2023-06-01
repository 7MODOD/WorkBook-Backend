using SQLite;

namespace WorkBook.Models
{
    public class WorkerAuthModel:BaseModel
    {
        
        
        public string Token { get; set; }
        public int WorkerId { get; set; }
        public WorkersModel Worker { get; set; }

    }
}
