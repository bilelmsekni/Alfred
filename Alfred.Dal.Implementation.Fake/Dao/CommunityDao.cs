using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alfred.Dal.Daos;
using Alfred.Dal.Entities.Communities;
using Alfred.Dal.Implementation.Fake.Database;
using Alfred.Dal.Implementation.Fake.EntityDtos;
using Alfred.Dal.Implementation.Fake.Filters;
using Alfred.Dal.Implementation.Fake.Mappers;

namespace Alfred.Dal.Implementation.Fake.Dao
{
    public class CommunityDao : ICommunityDao
    {        
        private readonly IEntityFactory _entityFactory;

        public CommunityDao(IEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        public async Task<IEnumerable<Community>> GetCommunities(CommunityCriteria criteria)
        {
            var dtos = await Task.Run(() => FakeCommunitiesDb.Communities).ConfigureAwait(false);
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
            return _entityFactory.TransformToCommunityEntity(await Task.Run(()=> FakeCommunitiesDb.Communities.FirstOrDefault(x => x.Id == id)).ConfigureAwait(false));
        }

        public async Task<int> SaveCommunity(Community community)
        {
            return await Task.Run(() =>
            {
                community.Id = FakeCommunitiesDb.Communities.Count + 1;
                FakeCommunitiesDb.Communities.Add(_entityFactory.TransformToCommunityDto(community));
                return community.Id;
            }).ConfigureAwait(false);
        }

        public async Task DeleteCommunity(int id)
        {
            await Task.Run(()=> FakeCommunitiesDb.Communities.RemoveAt(FakeCommunitiesDb.Communities.FindIndex(x => x.Id == id))).ConfigureAwait(false);
        }

        public async Task<Community> GetCommunity(string email)
        {
            return _entityFactory.TransformToCommunityEntity(await Task.Run(()=> FakeCommunitiesDb.Communities.FirstOrDefault(x => string.Equals(x.Email, email, StringComparison.InvariantCultureIgnoreCase))).ConfigureAwait(false));
        }

        public async Task UpdateCommunity(Community community)
        {
            await Task.Run(() =>
            {
                FakeCommunitiesDb.Communities.RemoveAt(FakeCommunitiesDb.Communities.FindIndex(x => x.Id == community.Id));
                FakeCommunitiesDb.Communities.Add(_entityFactory.TransformToCommunityDto(community));
            }).ConfigureAwait(false);
        }

        public async Task<int> CountCommunities(CommunityCriteria criteria)
        {
            return (await GetCommunities(criteria)).Count();
        }
    }
}
