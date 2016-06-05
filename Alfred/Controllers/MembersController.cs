using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Alfred.Dal.Entities;
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

        /// <summary>
        /// Get all members
        /// </summary>
        /// <remarks>
        /// Get all members
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<Member>))]
        public IHttpActionResult GetMembers()
        {
            var members = _memberRepo.GetMembers();
            return Ok(members);
        }
    }
}
