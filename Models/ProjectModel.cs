namespace WorkBook.Models
{
    public class ProjectModel:BaseModel
    {
        
        
        public string Title { get; set; }
        public string Description { get; set; }
        
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int DurationDays { get; set; }
        public string Address { get; set; }
        public int Price { get; set; }
        public string Status { get; set; }

        public int WorkerProfessionId { get; set; }
        public WorkerProfessionModel WorkerProfession { get; set; }
        public ICollection<CommentModel> Comments { get; set; }
        
        public int CustomerId { get; set; }
        public CustomerModel Customer { get; set; }

        public ICollection<RatingModel> Rating { get; set; }

    }
    public enum Status
    {
        PENDING,
        REJECTED,
        ACCEPTED,
        IN_PROGRESS,
        FINISHED
    }
}
