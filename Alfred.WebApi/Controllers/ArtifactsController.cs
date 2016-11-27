using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Alfred.Models;
using Alfred.Models.Artifacts;
using Alfred.Services;
using Alfred.Shared.Enums;
using Alfred.Shared.Extensions;
using FluentValidation;
using FluentValidation.WebApi;

namespace Alfred.WebApi.Controllers
{
    [RoutePrefix("artifacts")]
    public class ArtifactsController : ApiController
    {
        private readonly IArtifactService _artifactService;
        private readonly AbstractValidator<ArtifactCriteriaModel> _criteriaValidator;

        public ArtifactsController(IArtifactService artifactService,
            AbstractValidator<ArtifactCriteriaModel> criteriaValidator)
        {
            _artifactService = artifactService;
            _criteriaValidator = criteriaValidator;
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
        public async Task<IHttpActionResult> GetArtifacts(string ids = null, string title = null, 
            ArtifactType? artifactType = null, ArtifactStatus? artifactStatus= null, int? memberId = null, 
            int? communityId = null, int page = 1, int pageSize = 20)
        {
            var criteriaModel = new ArtifactCriteriaModel
            {
                Ids = ids?.SafeSplit(),
                Title = title,
                Type = artifactType,
                Status = artifactStatus,
                MemberId = memberId,
                CommunityId = communityId,
                Page = page,
                PageSize = pageSize                
            };

            var validationResults = _criteriaValidator.Validate(criteriaModel);
            if (validationResults.IsValid)
            {
                return Ok(await _artifactService.GetArtifacts(criteriaModel).ConfigureAwait(false));
            }

            validationResults.AddToModelState(ModelState, null);

            return BadRequest(ModelState);
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
