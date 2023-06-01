using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkBook.DTOs;
using WorkBook.repos;
using WorkBook.Services;
using WorkBook.Validators;


namespace WorkBook.Controllers
{
    [Route("customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("signup")]
        public ActionResult SignUp([FromBody] CustomerModelReq req)
        {
            var validator = new CustomerSignUpValidator();
            var result = validator.Validate(req);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors.ToString());
            }

            var customer = new CustomerServices();
            customer.SignUp(req);

            return Ok(new Dictionary<string, int> { { "status", 200 } });
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public ActionResult SignIn([FromBody] SignInReq req)
        {
            var validator = new SignInValidator();
            var result = validator.Validate(req);
            if (!result.IsValid)
            {
                throw new UnauthorizedAccessException(result.Errors.ToString());
            }

            var customer = new CustomerServices();
            var resp = customer.SignIn(req.email, req.password);
            return Ok(resp);
        }

        [HttpPut("{id}/img")]
        public async Task<ActionResult> UpdateCustomerImg(int id, [FromForm] IFormFile image)
        {

            var customer = new CustomerServices();
            if (image != null && image.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await image.CopyToAsync(stream);
                    var imgData = stream.ToArray();
                    customer.UpdateCustomerImg(id, imgData);
                }
            }
            else
            {
                customer.UpdateCustomerImg(id, null);
            }
            return Ok(new Dictionary<string, int> { { "status", 200 } });
        }

        [HttpGet("{id}/img")]
        public ActionResult GetCustomerImg(string id)
        {
            var customer = new CustomerServices();
            var img = customer.GetCustomerImg(int.Parse(id));
            return Ok(img);

        }

        [HttpGet("{id}")]
        public ActionResult GetCustomerProfile(string id)
        {
            var customer = new CustomerServices();
            var res = customer.GetCustomerProfile(int.Parse(id));
            return Ok(res);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCustomerProfile(string id, [FromBody] EditCustomerReq req)
        {
            var validator = new EditCustomerValidator();
            var result = validator.Validate(req);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors.ToString());
            }
            var customer = new CustomerServices();
            customer.UpdateCustomerProfile(int.Parse(id), req);
            return Ok(new Dictionary<string, int> { { "status", 200 } });
        }



        [HttpPost("{id}/projects")]
        public ActionResult CreateProject(string id, [FromBody] CreateProjectReq req)
        {
            var validator = new CreateProjectValidator();
            var result = validator.Validate(req);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors.ToString());
            }
            var project = new ProjectServices();
            project.CreateProject(int.Parse(id), req);
            return Ok(new Dictionary<string, int> { { "status", 200 } });
        }

        [HttpPut("{customerId}/project/{projectId}")]
        public ActionResult UpdateProject(string customerId, string projectId, [FromBody] EditProjectReq req )
        {
            var validator = new EditProjectValidator();
            var result = validator.Validate(req);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors.ToString());
            }
            var project = new ProjectServices();
            project.UpdateProjectByCustomer(int.Parse(projectId), int.Parse(customerId), req);
            return Ok(new Dictionary<string, int> { { "status", 200 } });

        }

        [HttpGet("{id}/projects")]
        public ActionResult GetCustomerProjects(string id, string? filter_by = "price", string? order = "ASC", string? status = "PENDING")
        {
            var project = new ProjectServices();
            var customerProjects = project.GetProjectsByCustomerId(int.Parse(id), filter_by, order, status);
            
            return Ok(customerProjects);
        }

        [HttpPost("{customerId}/projects/{projectId}/comments")]
        public ActionResult CreateCustomerComment(string customerId, string projectId, [FromBody] CommentReq text)
        {
            if (string.IsNullOrEmpty(text.text))
            {
                throw new ValidationException("the comment should not be empty");
            }
            var comment = new CommentServices();
            comment.CreateCustomerComment(text.text, int.Parse(customerId), int.Parse(projectId));
            return Ok(new Dictionary<string, int> { { "status", 200 } });
        }

        [HttpPost("{customerId}/projects/{projectId}/rate")]
        public ActionResult RateWorker(string customerId, string projectId, [FromBody] RateReq req)
        {
            var validator = new RatingValidator();
            var result = validator.Validate(req);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors.ToString());
            }
            var rate = new CustomerServices();
            rate.RateWorker(int.Parse(customerId), int.Parse(projectId), req);
            return Ok(new Dictionary<string, int> { { "status", 200 } });
        }


    }
}
