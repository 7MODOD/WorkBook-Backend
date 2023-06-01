using SQLite;

namespace WorkBook.Models
{
    public class ProfessionModel: BaseModel
    {
        
        public string Name { get; set; }
        public ICollection<WorkerProfessionModel> WorkerProfessions { get; set; }
    }
}
