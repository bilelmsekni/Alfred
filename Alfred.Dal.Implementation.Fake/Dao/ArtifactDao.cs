using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alfred.Dal.Daos;
using Alfred.Dal.Entities.Artifact;
using Alfred.Dal.Implementation.Fake.Database;
using Alfred.Dal.Implementation.Fake.EntityDtos;
using Alfred.Dal.Implementation.Fake.Filters;
using Alfred.Dal.Implementation.Fake.Mappers;

namespace Alfred.Dal.Implementation.Fake.Dao
{
    public class ArtifactDao : IArtifactDao
    {
        private readonly List<ArtifactDto> _artifacts = FakeDatabase.ArtifactData;
        private readonly IEntityFactory _entityFactory;

        public ArtifactDao(IEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        public async Task<int> CountArtifact(ArtifactCriteria artifactCriteria)
        {
            return (await GetArtifacts(artifactCriteria)).Count();
        }

        public async Task<IEnumerable<Artifact>> GetArtifacts(ArtifactCriteria artifactCriteria)
        {
            var dtos = await GetArtifacts();
            Func<ArtifactDto, bool> criteriafilters = artifact => true;

            return dtos.Where(criteriafilters
                .FilterOnIds(artifactCriteria.Ids)
                .FilterOnTitle(artifactCriteria.Title)
                .FilterOnType(artifactCriteria.Type)
                .FilterOnStatus(artifactCriteria.Status)
                .FilterOnMemberId(artifactCriteria.MemberId)
                .FilterOnCommunityId(artifactCriteria.CommunityId))
                .Select(_entityFactory.TransformToArtifactEntity);
        }

        public async Task<Artifact> GetArtifact(int id)
        {
            var artifact = await Task.Run(() => _artifacts.FirstOrDefault(x => x.Id == id)).ConfigureAwait(false);
            return _entityFactory.TransformToArtifactEntity(artifact);            
        }

        public async Task<int> SaveArtifact(Artifact artifact)
        {
            return await Task.Run(() =>
            {
                artifact.Id = _artifacts.Count + 1;
                _artifacts.Add(_entityFactory.TransformToArtifactDto(artifact));
                return artifact.Id;
            }).ConfigureAwait(false);
        }

        public async Task DeleteArtifact(int id)
        {
            await Task.Run(() => _artifacts.RemoveAt(_artifacts.FindIndex(x => x.Id == id))).ConfigureAwait(false);
        }

        public async Task UpdateArtifact(Artifact artifact)
        {
            await Task.Run(() =>
            {
                _artifacts.RemoveAt(_artifacts.FindIndex(x => x.Id == artifact.Id));
                _artifacts.Add(_entityFactory.TransformToArtifactDto(artifact));
            }).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Artifact>> GetMemberArtifacts(int id)
        {
            var dtos = await Task.Run(() => _artifacts.Where(x => x.MemberId == id)).ConfigureAwait(false);
            return dtos.Select(_entityFactory.TransformToArtifactEntity);
        }

        public async Task<IEnumerable<Artifact>> GetCommunityArtifacts(int id)
        {
            var dtos = await Task.Run(() => _artifacts.Where(x => x.CommunityId == id)).ConfigureAwait(false);
            return dtos.Select(_entityFactory.TransformToArtifactEntity);
        }

        private async Task<IEnumerable<ArtifactDto>> GetArtifacts()
        {
            return await Task.Run(() => _artifacts).ConfigureAwait(false);
        }
    }
}
