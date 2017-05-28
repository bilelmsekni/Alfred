using System.Threading.Tasks;
using Alfred.Shared.Standard.Constants;
using Alfred.Shared.Standard.Enums;
using Alfred.Shared.Standard.Extensions;
using Alfred.Standard.Models.Artifacts;
using Alfred.Standard.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Alfred.WebApi.NetCore.Controllers
{
    [Route("artifacts")]
    public class ArtifactsController : Controller
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
        [Route("", Name = AlfredRoutes.GetArtifacts)]
        [ProducesResponseType(typeof(ArtifactResponseModel), 200)]
        [ProducesResponseType(typeof(ArtifactResponseModel), 500)]
        public async Task<IActionResult> GetArtifacts(string ids = null, string title = null,
            ArtifactType? type = null, ArtifactStatus? status= null, int? memberId = null,
            int? communityId = null, int page = 1, int pageSize = 20)
        {
            var criteriaModel = new ArtifactCriteriaModel
            {
                Ids = ids?.SafeSplit(),
                Title = title,
                Type = type,
                Status = status,
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

            // validationResults.AddToModelState(ModelState, null);

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Get an artifact
        /// </summary>
        /// <remarks>
        /// Get an artifact
        /// </remarks>
        /// <param name="id">id of an artifact</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int?}")]
        [ProducesResponseType(typeof(ArtifactModel), 200)]
        [ProducesResponseType(typeof(ArtifactModel), 401)]
        public async Task<IActionResult> GetArtifact(int id)
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
        [ProducesResponseType(typeof(ArtifactModel), 200)]
        [ProducesResponseType(typeof(ArtifactModel), 500)]
        [Route("")]
        public async Task<IActionResult> CreateArtifact([FromBody]CreateArtifactModel createArtifactModel)
        {
            if (ModelState.IsValid)
            {
                var artifactId = await _artifactService.CreateArtifact(createArtifactModel).ConfigureAwait(false);

                if (artifactId != -1)
                {
                    return Created("", $"{Request.GetDisplayUrl()}/{artifactId}");
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
        /// <param name="updateArtifactModel">artifact data</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ArtifactModel), 200)]
        [ProducesResponseType(typeof(ArtifactModel), 500)]
        [Route("{id:int?}")]
        public async Task<IActionResult> UpdateArtifact(int id, [FromBody]UpdateArtifactModel updateArtifactModel)
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
        [ProducesResponseType(typeof(ArtifactModel), 200)]
        [ProducesResponseType(typeof(ArtifactModel), 401)]
        [Route("{id:int?}")]
        public async Task<IActionResult> DeleteArtifact(int id)
        {
            if (await _artifactService.DeleteArtifact(id).ConfigureAwait(false))
                return Ok();

            return NotFound();
        }
    }
}
