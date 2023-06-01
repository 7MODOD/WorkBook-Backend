using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkBook.Services;

namespace WorkBook.Controllers
{
    [Route("projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        [HttpGet("{id}/comments")]
        public ActionResult GetComments(string id)
        {
            var obj = new CommentServices();
            var comments = obj.GetComments(int.Parse(id));
            return Ok(comments);
        }

    }
}
