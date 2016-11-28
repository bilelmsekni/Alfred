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
        private readonly List<MemberDto> _members = FakeDatabase.Members;
        private readonly IEntityFactory _entityFactory;

        public MemberDao(IEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        public async Task<IEnumerable<Member>> GetMembers(MemberCriteria criteria)
        {
            var dtos = await Task.Run(() => _members).ConfigureAwait(false);
            Func<MemberDto, bool> criteriafilters = dto => true;

            return dtos.Where(criteriafilters
                .FilterOnIds(criteria.Ids)
                .FilterOnCommunityId(criteria.CommunityId)
                .FilterOnEmail(criteria.Email)
                .FilterOnName(criteria.Name)
                .FilterOnRole(criteria.Role))
                .Select(_entityFactory.TransformToMemberEntity);
        }

        public async Task<int> SaveMember(Member member)
        {
            return await Task.Run(() =>
            {
                member.Id = _members.Count + 1;
                _members.Add(_entityFactory.TransformToMemberDto(member));
                return member.Id;
            }).ConfigureAwait(false);
        }

        public async Task<Member> GetMember(string email)
        {
            return _entityFactory.TransformToMemberEntity(await Task.Run(() => _members.FirstOrDefault(x => x.Email.ToLowerInvariant() == email.ToLowerInvariant())).ConfigureAwait(false));
        }

        public async Task<Member> GetMember(int id)
        {
            return _entityFactory.TransformToMemberEntity(await Task.Run(() => _members.FirstOrDefault(x => x.Id == id)).ConfigureAwait(false));
        }

        public async Task UpdateMember(Member member)
        {
            await Task.Run(() =>
            {
                _members.RemoveAt(_members.FindIndex(x => x.Id == member.Id));
                _members.Add(_entityFactory.TransformToMemberDto(member));

            }).ConfigureAwait(false);
        }

        public async Task DeleteMember(int id)
        {
            await Task.Run(() => _members.RemoveAt(_members.FindIndex(x => x.Id == id))).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Member>> GetCommunityMembers(int id)
        {
            var dtos = await Task.Run(() => _members.Where(x => x.CommunityIds.Contains(id))).ConfigureAwait(false);
            return dtos.Select(_entityFactory.TransformToMemberEntity);
        }
    }
}
