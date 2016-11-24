using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alfred.Dal.Entities.Artifact;
using Alfred.Dal.FakeImplementation.Dao;
using Alfred.Dal.FakeImplementation.EntityDtos;
using Alfred.Dal.Interfaces;
using Alfred.Domain.Enums;

namespace Alfred.Dal.FakeImplementation.Repositories
{
    public class ArtifactRepository : IArtifactRepository
    {
        private readonly IArtifactDao _artifactDao;

        public ArtifactRepository(IArtifactDao artifactDao)
        {
            _artifactDao = artifactDao;
        }

        public async Task<IEnumerable<Artifact>> GetArtifacts()
        {
            var artifactDtos = await _artifactDao.GetArtifacts().ConfigureAwait(false);
            return artifactDtos.Select(TransformToArtifactEntity);
        }

        private static Artifact TransformToArtifactEntity(ArtifactDto artifactDto)
        {
            if (artifactDto == null) return null;

            return new Artifact
            {
                Id = artifactDto.Id,
                Title = artifactDto.Title,
                Bonus = artifactDto.Bonus,
                Reward = artifactDto.Reward,
                Status = (ArtifactStatus)artifactDto.Status,
                Type = (ArtifactType)artifactDto.Type
            };
        }

        public async Task<Artifact> GetArtifact(int id)
        {
            return TransformToArtifactEntity(await _artifactDao.GetArtifact(id).ConfigureAwait(false));
        }

        public async Task<int> SaveArtifact(Artifact artifact)
        {
            var artifactDto = TransformToArtifactDto(artifact);
            return await _artifactDao.SaveArtifact(artifactDto).ConfigureAwait(false);
        }

        private ArtifactDto TransformToArtifactDto(Artifact artifact)
        {
            if (artifact != null)
            {
                return new ArtifactDto
                {
                    Id = artifact.Id,
                    Title = artifact.Title,
                    Bonus = artifact.Bonus,
                    Reward = artifact.Reward,
                    Status = (int) artifact.Status,
                    Type = (int) artifact.Type
                };
            }
            return null;
        }

        public async Task DeleteArtifact(int id)
        {
            await _artifactDao.DeleteArtifact(id).ConfigureAwait(false);
        }

        public async Task UpdateArtifact(Artifact artifact)
        {
            var artifactDto = TransformToArtifactDto(artifact);
            await _artifactDao.UpdateArtifact(artifactDto).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Artifact>> GetMemberArtifacts(int id)
        {
            var artifactDtos = await _artifactDao.GetMemberArtifacts(id).ConfigureAwait(false);
            return artifactDtos.Select(TransformToArtifactEntity);
        }

        public async Task<IEnumerable<Artifact>> GetCommunityArtifacts(int id)
        {
            var communityArtifacts = await _artifactDao.GetCommunityArtifacts(id).ConfigureAwait(false);
            return communityArtifacts.Select(TransformToArtifactEntity).ToArray();
        }
    }
}
