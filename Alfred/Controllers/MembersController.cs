using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Alfred.Dal.Entities.Member;
using Alfred.Model;
using Alfred.Model.Members;
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
        /// Get a member
        /// </summary>
        /// <remarks>
        /// Get a member
        /// </remarks>
        /// <param name="id">member id</param>
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

        /// <summary>
        /// Create member
        /// </summary>
        /// <remarks>
        /// Create member
        /// </remarks>
        /// <param name="createMemberModel">member data</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(Member))]
        public IHttpActionResult CreateMember(CreateMemberModel createMemberModel)
        {
            if (ModelState.IsValid)
            {
                var memberModel = _memberService.CreateMember(createMemberModel);
                if (memberModel != null) return Created("", memberModel);
                return BadRequest("Something went wrong !");
            }
            return BadRequest("Something went wrong !");
        }

        /// <summary>
        /// Update member
        /// </summary>
        /// <remarks>
        /// Update member
        /// </remarks>
        /// <param name="UpdateMemberModel">update member data</param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(Member))]
        public IHttpActionResult UpdateMember(UpdateMemberModel updateMemberModel)
        {
            if (ModelState.IsValid)
            {
                var memberModel = _memberService.UpdateMember(updateMemberModel);
                if (memberModel != null) return Ok(memberModel);
                return BadRequest("Something went wrong !");
            }
            return BadRequest("Something went wrong !");
        }

        /// <summary>
        /// Delete member
        /// </summary>
        /// <remarks>
        /// Delete member
        /// </remarks>
        /// <param name="id">member id</param>
        /// <returns></returns>
        [HttpDelete]
        [ResponseType(typeof(Member))]
        public IHttpActionResult DeleteMember(int id)
        {
            if (_memberService.DeleteMember(id)) return Ok();
            return NotFound();
        }
    }
}
