using SQLite;

namespace WorkBook.Models
{
    public class RatingModel:BaseModel
    {
        public int CustomerId { get; set; }
        public CustomerModel Customer { get; set; }
        public int WorkerProfessionId { get; set; }
        public WorkerProfessionModel WorkerProfession { get; set; }
        [Unique]
        public int ProjectId { get; set; }
        public ProjectModel Project { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }

    }
}
