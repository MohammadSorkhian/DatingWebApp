using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class BuggyController : ControllerBase
    {
        private readonly DataContext context;
        public BuggyController(DataContext context) { 
            this.context = context;
        }

        [HttpGet]
        [Route("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret test";
        }

        [HttpGet]
        [Route("not-found")]
        public ActionResult<string> GetNotFound()
        {
            var thing = this.context.Users.Find(-1);
            // if (thing == null) return NotFound();
            thing.ToString();
            return NotFound();
        }

        [HttpGet]
        [Route("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest();
        }

    }
}