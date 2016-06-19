using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alfred.Dal.Entities.Artifact;
using Alfred.Dal.FakeImplementation.Dao;
using Alfred.Dal.FakeImplementation.EntityDtos;
using Alfred.Dal.Interfaces;

namespace Alfred.Dal.FakeImplementation.Repositories
{
    public class ArtifactRepository : IArtifactRepository
    {
        private readonly IArtifactDao _artifactDao;

        public ArtifactRepository(IArtifactDao artifactDao)
        {
            _artifactDao = artifactDao;
        }

        public IEnumerable<Artifact> GetArtifacts()
        {
            var artifactDtos = _artifactDao.GetArtifacts();
            return artifactDtos.Select(TransformToArtifactEntity);
        }

        private static Artifact TransformToArtifactEntity(ArtifactDto artifactDto)
        {
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

        public Artifact GetArtifact(int id)
        {
            return TransformToArtifactEntity(_artifactDao.GetArtifact(id));
        }

        public void SaveArtifact(Artifact artifact)
        {
            var artifactDto = TransformToArtifactDto(artifact);
            _artifactDao.SaveArtifact(artifactDto);
        }

        private ArtifactDto TransformToArtifactDto(Artifact artifact)
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

        public void DeleteArtifact(int id)
        {
            _artifactDao.DeleteArtifact(id);
        }

        public Artifact GetArtifact(string title)
        {
            return TransformToArtifactEntity(_artifactDao.GetArtifact(title));
        }

        public void UpdateArtifact(Artifact artifact)
        {
            var artifactDto = TransformToArtifactDto(artifact);
            _artifactDao.UpdateArtifact(artifactDto);
        }

        public IEnumerable<Artifact> GetMemberArtifacts(int id)
        {
            var artifactDtos = _artifactDao.GetMemberArtifacts(id);
            return artifactDtos.Select(TransformToArtifactEntity);
        }

        public IEnumerable<Artifact> GetCommunityArtifacts(int id)
        {
            return _artifactDao.GetCommunityArtifacts(id).Select(TransformToArtifactEntity);
        }
    }
}
