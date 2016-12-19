using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alfred.Dal.Daos;
using Alfred.Dal.Entities.Member;
using Alfred.Dal.Implementation.Fake.Database;
using Alfred.Dal.Implementation.Fake.EntityDtos;
using Alfred.Dal.Implementation.Fake.Filters;
using Alfred.Dal.Implementation.Fake.Mappers;

namespace Alfred.Dal.Implementation.Fake.Dao
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
            var dtos = await Task.Run(() => FakeMembersDb.Members).ConfigureAwait(false);
            Func<MemberDto, bool> criteriafilters = dto => true;

            return _entityFactory.TransformToMemberEntities(dtos.Where(criteriafilters
                .FilterOnIds(criteria.Ids)
                .FilterOnCommunityId(criteria.CommunityId)
                .FilterOnEmail(criteria.Email)
                .FilterOnName(criteria.Name)
                .FilterOnRole(criteria.Role)));                
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

        public async Task<Member> GetMember(string email)
        {
            return _entityFactory.TransformToMemberEntity(await Task.Run(() => FakeMembersDb.Members.FirstOrDefault(x => x.Email.ToLowerInvariant() == email.ToLowerInvariant())).ConfigureAwait(false));
        }

        public async Task<Member> GetMember(int id)
        {
            return _entityFactory.TransformToMemberEntity(await Task.Run(() => FakeMembersDb.Members.FirstOrDefault(x => x.Id == id)).ConfigureAwait(false));
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
