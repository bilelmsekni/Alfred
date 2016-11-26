using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alfred.Dal.Daos;
using Alfred.Dal.Implementation.Fake.Database;
using Alfred.Dal.Implementation.Fake.EntityDtos;
using Alfred.Dal.Implementation.Fake.Mappers;
using Alfred.Domain.Entities.Community;

namespace Alfred.Dal.Implementation.Fake.Dao
{
    public class CommunityDao : ICommunityDao
    {
        private readonly List<CommunityDto> _communities = FakeDatabase.Communities;
        private readonly IEntityFactory _entityFactory;

        public CommunityDao(IEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }
        public async Task<IEnumerable<Community>> GetCommunities()
        {
            var dtos = await Task.Run(() => _communities).ConfigureAwait(false);
            return dtos.Select(_entityFactory.TransformToCommunityEntity);
        }

        public async Task<Community> GetCommunity(int id)
        {
            return _entityFactory.TransformToCommunityEntity(await Task.Run(()=>_communities.FirstOrDefault(x => x.Id == id)).ConfigureAwait(false));
        }

        public async Task<int> SaveCommunity(Community community)
        {
            return await Task.Run(() =>
            {
                community.Id = _communities.Count + 1;
                _communities.Add(_entityFactory.TransformToCommunityDto(community));
                return community.Id;
            }).ConfigureAwait(false);
        }

        public async Task DeleteCommunity(int id)
        {
            await Task.Run(()=>_communities.RemoveAt(_communities.FindIndex(x => x.Id == id))).ConfigureAwait(false);
        }

        public async Task<Community> GetCommunity(string email)
        {
            return _entityFactory.TransformToCommunityEntity(await Task.Run(()=>_communities.FirstOrDefault(x => string.Equals(x.Email, email, StringComparison.InvariantCultureIgnoreCase))).ConfigureAwait(false));
        }

        public async Task UpdateCommunity(Community community)
        {
            await Task.Run(() =>
            {
                _communities.RemoveAt(_communities.FindIndex(x => x.Id == community.Id));
                _communities.Add(_entityFactory.TransformToCommunityDto(community));
            }).ConfigureAwait(false);
        }
    }
}
