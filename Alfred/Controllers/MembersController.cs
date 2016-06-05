using System.Web.Http;

namespace Alfred.Controllers
{
    [RoutePrefix("members")]
    public class MembersController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetMembers()
        {
            return Ok("Hello");
        }
    }
}
