using SQLite;

namespace WorkBook.Models
{
    public class WorkersModel: BaseModel
    {
        public string FirestName { get; set; }
        public string LastName { get; set; }
        [Unique]
        public string Email { get; set;}
        public string Password { get; set;}
        public string PhoneNumber { get; set;}
        public string Address1 { get; set;}
        public string? Address2 { get; set;}
        public string? Address3 { get; set;}
        public DateOnly DateOfBirth { get; set;}
        public byte[]? Img { get; set;}
        
        public WorkerAuthModel WorkerAuth { get; set; }
        public ICollection<WorkerProfessionModel> WorkerProfessions { get; set; }
        public ICollection<CommentModel> Comments { get; set; }

    }
}
