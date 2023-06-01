using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkBook.Services;

namespace WorkBook.Controllers
{
    [Route("professions")]
    [ApiController]
    public class ProfessionController : ControllerBase
    {
        [HttpGet("workers")]
        public ActionResult GetAllProfessionsWithWorkers()
        {
            var profession = new ProfessionServices();
            var res = profession.GetWorkersForProfession();
            return Ok(res);
        }

        [HttpGet("workers/tops")]
        public ActionResult GetTopWorkersInProfessions()
        {
            var profession = new ProfessionServices();
            var res = profession.GetTopWorkersForProfession();
            return Ok(res);
        }


    }
}
