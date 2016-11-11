using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Alfred.Model.Artifacts;
using Alfred.Services;

namespace Alfred.Controllers
{
    [RoutePrefix("artifacts")]
    public class ArtifactsController : ApiController
    {
        private readonly IArtifactService _artifactService;

        public ArtifactsController(IArtifactService artifactService)
        {
            _artifactService = artifactService;
        }

        /// <summary>
        /// Get all artifacts
        /// </summary>
        /// <remarks>
        /// Get all artifacts
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<ArtifactModel>))]
        public async Task<IHttpActionResult> GetArtifacts()
        {
            return Ok(await _artifactService.GetArtifacts().ConfigureAwait(false));
        }

        /// <summary>
        /// Get an artifacts
        /// </summary>
        /// <remarks>
        /// Get an artifacts
        /// </remarks>
        /// <param name="id">id of an artifact</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int?}")]
        [ResponseType(typeof(ArtifactModel))]
        public async Task<IHttpActionResult> GetArtifact(int id)
        {
            var artifact = await _artifactService.GetArtifact(id).ConfigureAwait(false);
            if (artifact != null)
                return Ok(artifact);
            return NotFound();
        }

        /// <summary>
        /// Create artifact
        /// </summary>
        /// <remarks>
        /// Create artifact
        /// </remarks>
        /// <param name="createArtifactModel">artifact data</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ArtifactModel))]
        [Route("")]
        public async Task<IHttpActionResult> CreateArtifact([FromBody]CreateArtifactModel createArtifactModel)
        {
            if (ModelState.IsValid)
            {
                var artifactId = await _artifactService.CreateArtifact(createArtifactModel).ConfigureAwait(false);

                if (artifactId != -1)
                {
                    return Created("", $"{Request.RequestUri.AbsoluteUri}/{artifactId}");
                }
                return BadRequest("Something went wrong !");
            }
            return BadRequest("Something went wrong !");
        }


        /// <summary>
        /// Update artifact
        /// </summary>
        /// <remarks>
        /// Update artifact
        /// </remarks>
        /// <param name="id">artifact id</param>
        /// <param name="updatertifactModel">artifact data</param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(ArtifactModel))]
        [Route("{id:int?}")]
        public async Task<IHttpActionResult> UpdateArtifact(int id, [FromBody]UpdateArtifactModel updateArtifactModel)
        {            
            if (ModelState.IsValid)
            {
                updateArtifactModel.Id = id;
                var artifact = await _artifactService.UpdateArtifact(updateArtifactModel).ConfigureAwait(false);
                if (artifact != null) return Ok(artifact);
                return BadRequest("Something went wrong !");
            }
            return BadRequest("Something went wrong !");
        }

        /// <summary>
        /// Delete artifact
        /// </summary>
        /// <remarks>
        /// Delete artifact
        /// </remarks>
        /// <param name="id">artifact id</param>
        /// <returns></returns>
        [HttpDelete]
        [ResponseType(typeof(ArtifactModel))]
        [Route("{id:int?}")]
        public async Task<IHttpActionResult> DeleteArtifact(int id)
        {
            if (await _artifactService.DeleteArtifact(id).ConfigureAwait(false))
                return Ok();

            return NotFound();
        }
    }
}
