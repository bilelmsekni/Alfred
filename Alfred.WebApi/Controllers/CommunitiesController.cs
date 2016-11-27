using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Alfred.Models.Communities;
using Alfred.Services;
using Alfred.Shared.Extensions;
using FluentValidation;
using FluentValidation.WebApi;

namespace Alfred.WebApi.Controllers
{
    [RoutePrefix("communities")]
    public class CommunitiesController : ApiController
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
        [ResponseType(typeof(IEnumerable<CommunityModel>))]
        public async Task<IHttpActionResult> GetCommunities(string ids = null, string email = null, string name = null)
        {
            var criteria = new CommunityCriteriaModel
            {
                Ids = ids.SafeSplit(),
                Email = email,
                Name = name
            };

            var validationResults =_criteriaValidator.Validate(criteria);
            if (validationResults.IsValid)
            {
                return Ok(await _communityService.GetCommunities(criteria).ConfigureAwait(false));
            }
            validationResults.AddToModelState(ModelState, null);
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
        [ResponseType(typeof(CommunityModel))]
        public async Task<IHttpActionResult> GetCommunity(int id)
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
        [ResponseType(typeof(CommunityModel))]
        [Route("")]
        public async Task<IHttpActionResult> CreateCommunity([FromBody]CreateCommunityModel createCommunityModel)
        {
            if (ModelState.IsValid)
            {
                var communityId = await _communityService.CreateCommunity(createCommunityModel).ConfigureAwait(false);
                if (communityId != -1) return Created("", $"{Request.RequestUri.AbsoluteUri}/{communityId}");
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
        /// <param name="updateCommunityModel">community data</param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(CommunityModel))]
        [Route("{id:int?}")]
        public async Task<IHttpActionResult> UpdateCommunity(int id, [FromBody]UpdateCommunityModel updateCommunityModel)
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
        /// <param name="createCommunityModel">community data</param>
        /// <returns></returns>
        [HttpDelete]
        [ResponseType(typeof(CommunityModel))]
        [Route("{id:int?}")]
        public async Task<IHttpActionResult> DeleteCommunity(int id)
        {
            if (await _communityService.DeleteCommunity(id).ConfigureAwait(false)) return Ok();
            return NotFound();
        }
    }
}
