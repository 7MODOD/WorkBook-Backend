using SQLite;

namespace WorkBook.Models
{
    public class CustomerModel: BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address3 { get; set;}
        [Unique]
        public string Email { get; set; }
        public string Password { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string HomePhoneNumber { get; set;}
        public byte[]? Img { get; set; }
        public CustomerAuthModel CustomerAuth { get; set; }
        public ICollection<ProjectModel> Projects { get; set; }
        public ICollection<CommentModel> Comments { get; set; }
        public ICollection<RatingModel> Rating { get; set; }


    }
}
