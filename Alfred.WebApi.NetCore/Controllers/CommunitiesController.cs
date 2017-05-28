using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Shared.Standard.Extensions;
using Alfred.Standard.Models.Communities;
using Alfred.Standard.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Alfred.WebApi.NetCore.Controllers
{
    [Route("communities")]
    public class CommunitiesController : Controller
    {
        private readonly ICommunityService _communityService;
        private readonly AbstractValidator<CommunityCriteriaModel> _criteriaValidator;


        public CommunitiesController(ICommunityService communityService, AbstractValidator<CommunityCriteriaModel> criteriaValidator)
        {
            _communityService = communityService;
            _criteriaValidator = criteriaValidator;
        }

        /// <summary>
        /// Get communities
        /// </summary>
        /// <remarks>
        /// Get communities
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<CommunityModel>), 200)]
        public async Task<IActionResult> GetCommunities(string ids = null, string email = null, string name = null, int page = 1, int pageSize = 50)
        {
            var criteria = new CommunityCriteriaModel
            {
                Ids = ids?.SafeSplit(),
                Email = email,
                Name = name,
                Page = page,
                PageSize = pageSize
            };

            var validationResults =_criteriaValidator.Validate(criteria);
            if (validationResults.IsValid)
            {
                return Ok(await _communityService.GetCommunities(criteria).ConfigureAwait(false));
            }
            // validationResults.AddToModelState(ModelState, null);
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Get a community
        /// </summary>
        /// <remarks>
        /// Get a community
        /// </remarks>
        /// <param name="id">community id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int?}")]
        [ProducesResponseType(typeof(CommunityModel), 200)]
        public async Task<IActionResult> GetCommunity(int id)
        {
            var community = await _communityService.GetCommunity(id).ConfigureAwait(false);
            if (community != null)
                return Ok(community);
            return NotFound();
        }

        /// <summary>
        /// Create community
        /// </summary>
        /// <remarks>
        /// Create community
        /// </remarks>
        /// <param name="createCommunityModel">community data</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CommunityModel), 200)]
        [Route("")]
        public async Task<IActionResult> CreateCommunity([FromBody]CreateCommunityModel createCommunityModel)
        {
            if (ModelState.IsValid)
            {
                var communityId = await _communityService.CreateCommunity(createCommunityModel).ConfigureAwait(false);
                if (communityId != -1) return Created("", $"{Request.GetDisplayUrl()}/{communityId}");
                return BadRequest("Something went wrong !");
            }
            return BadRequest("Something went wrong !");
        }

        /// <summary>
        /// Update community
        /// </summary>
        /// <remarks>
        /// Update community
        /// </remarks>
        /// <param name="updateCommunityModel">id</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(CommunityModel), 200)]
        [Route("{id:int?}")]
        public async Task<IActionResult> UpdateCommunity(int id, [FromBody]UpdateCommunityModel updateCommunityModel)
        {
            if (ModelState.IsValid)
            {
                updateCommunityModel.Id = id;
                var community = await _communityService.UpdateCommunity(updateCommunityModel).ConfigureAwait(false);
                if (community != null) return Ok(community);
                return BadRequest("Something went wrong !");
            }
            return BadRequest("Something went wrong !");
        }

        /// <summary>
        /// Update community
        /// </summary>
        /// <remarks>
        /// Update community
        /// </remarks>
        /// <param name="id">community data</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(CommunityModel), 200)]
        [ProducesResponseType(typeof(CommunityModel), 401)]
        [Route("{id:int?}")]
        public async Task<IActionResult> DeleteCommunity(int id)
        {
            if (await _communityService.DeleteCommunity(id).ConfigureAwait(false)) return Ok();
            return NotFound();
        }
    }
}
