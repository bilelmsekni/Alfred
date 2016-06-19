using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Alfred.Model.Members;
using Alfred.Services;

namespace Alfred.Controllers
{
    [RoutePrefix("members")]
    public class MembersController : ApiController
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
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
        [ResponseType(typeof(IEnumerable<MemberModel>))]
        public IHttpActionResult GetMembers()
        {
            return Ok(_memberService.GetMembers());            
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
        [ResponseType(typeof(MemberModel))]
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
        [ResponseType(typeof(MemberModel))]
        [Route("")]
        public IHttpActionResult CreateMember([FromBody]CreateMemberModel createMemberModel)
        {
            if (ModelState.IsValid)
            {
                var memberId = _memberService.CreateMember(createMemberModel);
                if (memberId != -1) return Created("", $"{Request.RequestUri.AbsoluteUri}/{memberId}");
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
        [ResponseType(typeof(MemberModel))]
        [Route("{id:int?}")]
        public IHttpActionResult UpdateMember(int id, [FromBody]UpdateMemberModel updateMemberModel)
        {
            if (ModelState.IsValid)
            {
                updateMemberModel.Id = id;
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
        [Route("{id:int?}")]
        public IHttpActionResult DeleteMember(int id)
        {
            if (_memberService.DeleteMember(id)) return Ok();
            return NotFound();
        }
    }
}
