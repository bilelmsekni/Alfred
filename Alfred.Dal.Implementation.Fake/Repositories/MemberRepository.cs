using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alfred.Dal.Entities.Enums;
using Alfred.Dal.Entities.Member;
using Alfred.Dal.Implementation.Fake.Dao;
using Alfred.Dal.Implementation.Fake.EntityDtos;
using Alfred.Dal.Interfaces;

namespace Alfred.Dal.Implementation.Fake.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly IMemberDao _memberDao;

        public MemberRepository(IMemberDao memberDao)
        {
            _memberDao = memberDao;
        }
        public async Task<IEnumerable<Member>> GetMembers()
        {
            var members = await _memberDao.GetMembers().ConfigureAwait(false);
            return members.Select(TransformToMemberEntity).ToArray();
        }

        private Member TransformToMemberEntity(MemberDto memberDto)
        {
            if (memberDto != null)
            {
                return new Member
                {
                    Id = memberDto.Id,
                    Email = memberDto.Email,
                    FirstName = memberDto.FirstName,
                    LastName = memberDto.LastName,
                    Role = (CommunityRole)memberDto.Role
                };
            }
            return null;
        }

        public async Task<Member> GetMember(int id)
        {
            return TransformToMemberEntity(await _memberDao.GetMember(id).ConfigureAwait(false));
        }

        public async Task<int> SaveMember(Member member)
        {
            var memberDto = TransformToMemberDto(member);
            return await _memberDao.SaveMember(memberDto).ConfigureAwait(false);
        }

        private MemberDto TransformToMemberDto(Member member)
        {
            return new MemberDto
            {
                Id = member.Id,
                Email = member.Email,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Role = (int)member.Role
            };
        }

        public async Task DeleteMember(int id)
        {
            await _memberDao.DeleteMember(id).ConfigureAwait(false);
        }

        public async Task<Member> GetMember(string email)
        {
            return TransformToMemberEntity(await _memberDao.GetMember(email).ConfigureAwait(false));
        }

        public async Task UpdateMember(Member member)
        {
            var memberDto = TransformToMemberDto(member);
            await _memberDao.UpdateMember(memberDto).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Member>> GetCommunityMembers(int id)
        {
            var communityMembers = await _memberDao.GetCommunityMembers(id).ConfigureAwait(false);
            return communityMembers.Select(TransformToMemberEntity);
        }
    }
}
