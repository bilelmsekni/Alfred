using System.Web.Http;
using Alfred.Dal.Interfaces;

namespace Alfred.Controllers
{
    [RoutePrefix("members")]
    public class MembersController : ApiController
    {
        private readonly IMemberRepository _memberRepo;

        public MembersController(IMemberRepository memeberRepo)
        {
            _memberRepo = memeberRepo;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetMembers()
        {
            var members = _memberRepo.GetMembers();
            return Ok(members);
        }
    }
}
