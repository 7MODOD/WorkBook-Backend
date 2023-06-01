using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
using WorkBook.DTOs;
using WorkBook.repos;
using WorkBook.Services;
using WorkBook.Validators;

namespace WorkBook.Controllers
{
    [Route("worker")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("signup")]
        public ActionResult SignUp([FromBody] WorkersModelReq req)
        {
            var validator = new WorkerSignUpValidator();
            var result = validator.Validate(req);
            if(!result.IsValid)
            {
                throw new ValidationException(result.Errors.ToString());
            }

            var worker = new WorkerServices();
            worker.SignUp(req);

            return Ok(new Dictionary<string, int> { { "status", 200 } });
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public ActionResult SignIn([FromBody] SignInReq req)
        {
            var validator = new SignInValidator();
            var result = validator.Validate(req);
            if(!result.IsValid)
            {
                throw new UnauthorizedAccessException(result.Errors.ToString());
            }

            var worker = new WorkerServices();
            var resp = worker.SignIn(req.email, req.password);
            return Ok(resp);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateWorkerProfile(string id, [FromBody] EditWorkerReq req)
        {
            var validator = new EditWorkerValidator();
            var result = validator.Validate(req);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors.ToString());
            }
            var worker = new WorkerServices();
            worker.UpdateWorkerProfile(int.Parse(id), req);
            return Ok(new Dictionary<string, int> { { "status", 200 } });
        }

        [HttpGet("{id}")]
        public ActionResult GetWorkerProfile(string id)
        {
            var worker = new WorkerServices();
            var res = worker.GetWorkerProfile(int.Parse(id));
            return Ok(res);
        }

        [HttpPut("{id}/img")]
        public async Task<ActionResult> UpdateWorkerImg(int id, [FromForm] IFormFile image)
        {

            var worker = new WorkerServices();
            if (image != null && image.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await image.CopyToAsync(stream);
                    var imgData = stream.ToArray();
                    worker.UpdateWorkerImg(id, imgData);
                }
            }
            else
            {
                worker.UpdateWorkerImg(id, null);
            }
            return Ok(new Dictionary<string, int> { { "status", 200 } });
        }

        [HttpGet("{id}/img")]
        public ActionResult GetWorkerImg(string id)
        {
            var worker = new WorkerServices();
            var img = worker.GetWorkerImg(int.Parse(id));
            return Ok(img);

        }


        [HttpPut("{workerId}/project/{projectId}")]
        public ActionResult UpdateProject(string workerId, string projectId, [FromBody] EditProjectReq req)
        {
            var validator = new EditProjectValidator();
            var result = validator.Validate(req);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors.ToString());
            }
            var project = new ProjectServices();
            project.UpdateProjectByWorker(int.Parse(projectId), int.Parse(workerId), req);
            return Ok(new Dictionary<string, int> { { "status", 200 } });

        }

        [HttpGet("{id}/projects")]/////////////////////////////////////////
        public ActionResult GetWorkerProjects(string id, string? filter_by = "price", string? order = "ASC", string? status = null)
        {
            var project = new ProjectServices();
            var workerProjects = project.GetProjectsByWorkerId(int.Parse(id), filter_by, order, status);

            return Ok(workerProjects);
        }

        [HttpPost("{workerId}/projects/{projectId}/comments")]
        public ActionResult CreateWorkerComment(string workerId, string projectId, [FromBody] CommentReq text)
        {
            
            if (string.IsNullOrEmpty(text.text))
            {
                throw new ValidationException("the comment should not be empty");
            }
            var comment = new CommentServices();
            comment.CreateWorkerComment(text.text, int.Parse(workerId), int.Parse(projectId));
            return Ok(new Dictionary<string, int> { { "status", 200 } });
        }



    }
    }
