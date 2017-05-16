using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alfred.Dal.Implementation.Fake.Standard.Database;
using Alfred.Dal.Implementation.Fake.Standard.EntityDtos;
using Alfred.Dal.Implementation.Fake.Standard.Filters;
using Alfred.Dal.Implementation.Fake.Standard.Mappers;
using Alfred.Dal.Standard.Daos;
using Alfred.Dal.Standard.Entities.Members;

namespace Alfred.Dal.Implementation.Fake.Standard.Dao
{
    public class MemberDao : IMemberDao
    {
        private readonly IEntityFactory _entityFactory;

        public MemberDao(IEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        public async Task<IEnumerable<Member>> GetMembers(MemberCriteria criteria)
        {
            var dtos = await Task.FromResult(FakeMembersDb.Members).ConfigureAwait(false);

            Func<MemberDto, bool> criteriafilters = dto => true;
            var filteredDtos = dtos.Where(criteriafilters
                .FilterOnIds(criteria.Ids)
                .FilterOnCommunityId(criteria.CommunityId)
                .FilterOnEmail(criteria.Email)
                .FilterOnName(criteria.Name)
                .FilterOnRole(criteria.Role));

            return await ConvertDtos(filteredDtos).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Member>> ConvertDtos(IEnumerable<MemberDto> filteredDtos)
        {
            var members = GroupMembers(filteredDtos);
            var memberCommunities = await GroupMemberCommunities(filteredDtos).ConfigureAwait(false);

            var results = new List<Member>();
            foreach (var memberId in members.Keys)
            {
                results.Add(_entityFactory.TransformToMemberEntity(members[memberId], memberCommunities[memberId]));
            }
            return results;
        }

        private async Task<Dictionary<int, List<CommunityDto>>> GroupMemberCommunities(IEnumerable<MemberDto> members)
        {
            var communities = await Task.FromResult(FakeCommunitiesDb.Communities).ConfigureAwait(false);

            var result = new Dictionary<int, List<CommunityDto>>();

            foreach (var member in members)
            {
                if (result.ContainsKey(member.Id))
                {
                    result[member.Id].AddRange(communities.Where(c => c.Id == member.CommunityId));
                }
                else
                {
                    result[member.Id] = communities.Where(c => c.Id == member.CommunityId).ToList();
                }
            }
            return result;
        }

        private Dictionary<int, MemberDto> GroupMembers(IEnumerable<MemberDto> members)
        {
            var result = new Dictionary<int, MemberDto>();
            foreach (var member in members)
            {
                if (!result.ContainsKey(member.Id)) result[member.Id] = member;
            }
            return result;
        }

        public async Task<int> SaveMember(Member member)
        {
            return await Task.Run(() =>
            {
                member.Id = FakeMembersDb.Members.Count + 1;
                FakeMembersDb.Members.Add(_entityFactory.TransformToMemberDto(member));
                return member.Id;
            }).ConfigureAwait(false);
        }

        public async Task<Member> GetMember(int id)
        {
            var dtos = await Task.FromResult(FakeMembersDb.Members.Where(x => x.Id == id)).ConfigureAwait(false);

            return (await ConvertDtos(dtos).ConfigureAwait(false)).FirstOrDefault();
        }

        public async Task<Member> GetMember(string email)
        {
            var dtos = await Task.FromResult(FakeMembersDb.Members.Where(x => x.Email == email)).ConfigureAwait(false);

            return (await ConvertDtos(dtos).ConfigureAwait(false)).FirstOrDefault();
        }

        public async Task UpdateMember(Member member)
        {
            await Task.Run(() =>
            {
                FakeMembersDb.Members.RemoveAt(FakeMembersDb.Members.FindIndex(x => x.Id == member.Id));
                FakeMembersDb.Members.Add(_entityFactory.TransformToMemberDto(member));

            }).ConfigureAwait(false);
        }

        public async Task DeleteMember(int id)
        {
            await Task.Run(() => FakeMembersDb.Members.RemoveAt(FakeMembersDb.Members.FindIndex(x => x.Id == id))).ConfigureAwait(false);
        }

        public async Task<int> CountMembers(MemberCriteria criteria)
        {
            return (await GetMembers(criteria)).Count();
        }
    }
}
