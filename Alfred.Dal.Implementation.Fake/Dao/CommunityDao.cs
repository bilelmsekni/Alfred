using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alfred.Dal.Daos;
using Alfred.Dal.Entities.Community;
using Alfred.Dal.Implementation.Fake.Database;
using Alfred.Dal.Implementation.Fake.EntityDtos;
using Alfred.Dal.Implementation.Fake.Filters;
using Alfred.Dal.Implementation.Fake.Mappers;

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
        public async Task<IEnumerable<Community>> GetCommunities(CommunityCriteria criteria)
        {
            var dtos = await Task.Run(() => _communities).ConfigureAwait(false);
            Func<CommunityDto, bool> criteriaFilters = dto => true;

            return dtos.Where(
                criteriaFilters
                .FilterOnIds(criteria.Ids)
                .FilterOnEmail(criteria.Email)
                .FilterOnName(criteria.Name))
                .Select(_entityFactory.TransformToCommunityEntity);
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

        public async Task<int> CountCommunities(CommunityCriteria criteria)
        {
            return (await GetCommunities(criteria)).Count();
        }
    }
}
