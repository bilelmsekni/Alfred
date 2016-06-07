using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Alfred.Dal.Entities;
using Alfred.Services;

namespace Alfred.Controllers
{
    [RoutePrefix("members")]
    public class MembersController : ApiController
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memeberService)
        {
            _memberService = memeberService;
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
            var members = _memberService.GetMembers();
            return Ok(members);
        }

        /// <summary>
        /// Get all members
        /// </summary>
        /// <remarks>
        /// Get all members
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int?}")]
        [ResponseType(typeof(Member))]
        public IHttpActionResult GetMember(int id)
        {
            var member = _memberService.GetMember(id);
            if (member != null)
                return Ok(member);
            return NotFound();
        }
    }
}
