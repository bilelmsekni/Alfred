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

        /// <summary>
        /// Update community
        /// </summary>
        /// <remarks>
        /// Update community
        /// </remarks>
        /// <param name="createCommunityModel">community data</param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(CommunityModel))]
        [Route("{id:int?}")]
        public IHttpActionResult UpdateCommunity(int id, [FromBody]UpdateCommunityModel createCommunityModel)
        {
            if (ModelState.IsValid)
            {
                var community = _communityService.UpdateCommunity(createCommunityModel);
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
        public IHttpActionResult DeleteCommunity(int id)
        {
            if (_communityService.DeleteCommunity(id)) return Ok();            
            return NotFound();
        }
    }
}
