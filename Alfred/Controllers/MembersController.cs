using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<IHttpActionResult> GetMembers()
        {
            return Ok(await _memberService.GetMembers().ConfigureAwait(false));            
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
        public async Task<IHttpActionResult> GetMember(int id)
        {
            var member = await _memberService.GetMember(id).ConfigureAwait(false);
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
        public async Task<IHttpActionResult> CreateMember([FromBody]CreateMemberModel createMemberModel)
        {
            if (ModelState.IsValid)
            {
                var memberId = await _memberService.CreateMember(createMemberModel).ConfigureAwait(false);
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
        public async Task<IHttpActionResult> UpdateMember(int id, [FromBody]UpdateMemberModel updateMemberModel)
        {
            if (ModelState.IsValid)
            {
                updateMemberModel.Id = id;
                var memberModel = await _memberService.UpdateMember(updateMemberModel).ConfigureAwait(false);
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
        public async Task<IHttpActionResult> DeleteMember(int id)
        {
            if (await _memberService.DeleteMember(id).ConfigureAwait(false)) return Ok();
            return NotFound();
        }
    }
}