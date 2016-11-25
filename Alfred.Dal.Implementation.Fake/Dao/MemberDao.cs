using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alfred.Dal.Implementation.Fake.Database;
using Alfred.Dal.Implementation.Fake.EntityDtos;

namespace Alfred.Dal.Implementation.Fake.Dao
{
    public class MemberDao : IMemberDao
    {
        private readonly List<MemberDto> _members = FakeDatabase.Members;

        public async Task<IEnumerable<MemberDto>> GetMembers()
        {
            return await Task.Run(() => _members).ConfigureAwait(false);
        }

        public async Task<int> SaveMember(MemberDto member)
        {
            return await Task.Run(() =>
            {
                member.Id = _members.Count + 1;
                _members.Add(member);
                return member.Id;
            }).ConfigureAwait(false);
        }

        public async Task<MemberDto> GetMember(string email)
        {
            return await Task.Run(() => _members.FirstOrDefault(x => x.Email.ToLowerInvariant() == email.ToLowerInvariant())).ConfigureAwait(false);
        }

        public async Task<MemberDto> GetMember(int id)
        {
            return await Task.Run(() => _members.FirstOrDefault(x => x.Id == id)).ConfigureAwait(false);
        }

        public async Task UpdateMember(MemberDto member)
        {
            await Task.Run(() =>
            {
                _members.RemoveAt(_members.FindIndex(x => x.Id == member.Id));
                _members.Add(member);

            }).ConfigureAwait(false);
        }

        public async Task DeleteMember(int id)
        {
            await Task.Run(() => _members.RemoveAt(_members.FindIndex(x => x.Id == id))).ConfigureAwait(false);
        }

        public async Task<IEnumerable<MemberDto>> GetCommunityMembers(int id)
        {
            return await Task.Run(() => _members.Where(x => x.CommunityIds.Contains(id))).ConfigureAwait(false);
        }
    }
}
