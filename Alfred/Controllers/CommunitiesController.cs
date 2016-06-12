using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Alfred.Model.Communities;
using Alfred.Services;

namespace Alfred.Controllers
{
    [RoutePrefix("communities")]
    public class CommunitiesController : ApiController
    {
        private readonly ICommunityService _communityService;

        public CommunitiesController(ICommunityService communityService)
        {
            _communityService = communityService;
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
        public IHttpActionResult GetCommunities()
        {
            return Ok(_communityService.GetCommunities());
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
        [Route("{int:int?}")]
        [ResponseType(typeof(CommunityModel))]
        public IHttpActionResult GetCommunity(int id)
        {
            var community = _communityService.GetCommunity(id);
            if (community != null)
                return Ok(community);
            return NotFound();
        }

        /// <summary>
        /// Create member
        /// </summary>
        /// <remarks>
        /// Create member
        /// </remarks>
        /// <param name="createCommunityModel">member data</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(CommunityModel))]
        [Route("")]
        public IHttpActionResult CreateCommunity([FromBody]CreateCommunityModel createCommunityModel)
        {
            if (ModelState.IsValid)
            {
                var community = _communityService.CreateCommunity(createCommunityModel);
                if (community != null) return Created("", community);
                return BadRequest("Something went wrong !");
            }
            return BadRequest("Something went wrong !");
        }
    }
}
