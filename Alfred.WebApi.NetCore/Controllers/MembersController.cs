using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Shared.Standard.Enums;
using Alfred.Shared.Standard.Extensions;
using Alfred.Standard.Models.Members;
using Alfred.Standard.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Alfred.WebApi.NetCore.Controllers
{
    [Route("members")]
    public class MembersController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly AbstractValidator<MemberCriteriaModel> _criteriaValidator;

        public MembersController(IMemberService memberService, AbstractValidator<MemberCriteriaModel> criteriaValidator)
        {
            _memberService = memberService;
            _criteriaValidator = criteriaValidator;
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
        [ProducesResponseType(typeof(IEnumerable<MemberModel>), 200)]
        public async Task<IActionResult> GetMembers(string ids = null, string email = null,
            string name = null, CommunityRole? role = null, int? communityId = null, int page = 1, int pageSize = 20)
        {
            var memberCriteria = new MemberCriteriaModel
            {
                Ids = ids?.SafeSplit(),
                Email = email,
                Role = role,
                Name = name,
                CommunityId = communityId,
                Page = page,
                PageSize = pageSize
            };
            var validationResults = _criteriaValidator.Validate(memberCriteria);
            if (validationResults.IsValid)
            {
                return Ok(await _memberService.GetMembers(memberCriteria).ConfigureAwait(false));
            }

            // validationResults.AddToModelState(ModelState, null);
            return BadRequest(ModelState);
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
        [ProducesResponseType(typeof(MemberModel), 200)]
        public async Task<IActionResult> GetMember(int id)
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
        [ProducesResponseType(typeof(MemberModel), 200)]
        [Route("")]
        public async Task<IActionResult> CreateMember([FromBody]CreateMemberModel createMemberModel)
        {
            if (ModelState.IsValid)
            {
                var memberId = await _memberService.CreateMember(createMemberModel).ConfigureAwait(false);
                if (memberId != -1) return Created("", $"{Request.GetDisplayUrl()}/{memberId}");
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
        /// <param name="id">update member data</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(MemberModel), 200)]
        [Route("{id:int?}")]
        public async Task<IActionResult> UpdateMember(int id, [FromBody]UpdateMemberModel updateMemberModel)
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
        public async Task<IActionResult> DeleteMember(int id)
        {
            if (await _memberService.DeleteMember(id).ConfigureAwait(false)) return Ok();
            return NotFound();
        }
    }
}