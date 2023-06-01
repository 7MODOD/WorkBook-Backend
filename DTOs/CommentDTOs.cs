namespace WorkBook.DTOs
{
    
    public record CommentResp(int id, string username, string text);

    public record CommentReq(string text);
}
