namespace WorkBook.Models
{
    public class CommentModel:BaseModel
    {
        public string Comment { get; set; }
        public int? CustomerId { get; set; }
        public CustomerModel? Customer { get; set; }
        public int? WorkerId { get; set; }
        public WorkersModel? Worker { get; set; }
        public int ProjectId { get; set; }
        public ProjectModel Project { get; set; }

    }
}
