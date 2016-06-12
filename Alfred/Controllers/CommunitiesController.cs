using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Alfred.Dal.Interfaces;
using Alfred.Model.Communities;

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

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<CommunityModel>))]
        public IHttpActionResult GetCommunities()
        {
            return Ok(_communityService.GetCommunities());
        }
    }
}
