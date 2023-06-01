using WorkBook.DTOs;
using WorkBook.Models;
using WorkBook.repos;

namespace WorkBook.Services
{
    public class CommentServices
    {
        private DbServices _db;

        public CommentServices()
        {
            _db = new DbServices();
        }


        public void CreateCustomerComment(string value, int customerId, int projectId)
        {
            var comment = _db.CreateCustomerComment(value, customerId, projectId);
            _db.AddRecord(comment);

        }
        public void CreateWorkerComment(string value, int workerId, int projectId)
        {
            var comment = _db.CreateWorkerComment(value, workerId, projectId);
            _db.AddRecord(comment);

        }

        public IEnumerable<CommentResp> GetComments(int projectId)
        {
            var comments = _db.GetComments(projectId);
            var orderedComments = OrderComments(comments);

            IEnumerable<CommentResp> resp = _db.CreateCommentResp(orderedComments);

            return resp;
        }

        private IEnumerable<CommentModel> OrderComments(List<CommentModel> comments)
        {
            var orderedComments = comments.OrderByDescending(c => c.CreatedAt).ToList();
            IEnumerable<CommentModel> ordered = orderedComments;
            return ordered;

        }


    }
}
