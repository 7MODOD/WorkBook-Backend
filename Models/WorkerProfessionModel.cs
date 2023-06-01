namespace WorkBook.Models
{
    public class WorkerProfessionModel:BaseModel
    {
        public int WorkerId { get; set; }
        public WorkersModel Worker { get; set; }
        public int ProfessionId { get; set; }
        public ProfessionModel Profession { get; set; }
        public ICollection<ProjectModel> Projects { get; set; }
        public ICollection<RatingModel> Rating { get; set; }

    }
}
