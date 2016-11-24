using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alfred.Dal.Entities.Enums;
using Alfred.Dal.Entities.Member;
using Alfred.Dal.FakeImplementation.Dao;
using Alfred.Dal.FakeImplementation.EntityDtos;
using Alfred.Dal.Interfaces;

namespace Alfred.Dal.FakeImplementation.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly IMemberDao _memberDao;
        private readonly IArtifactRepository _artifactRepository;

        public MemberRepository(IMemberDao memberDao, IArtifactRepository artifactRepository)
        {
            _memberDao = memberDao;
            _artifactRepository = artifactRepository;
        }
        public async Task<IEnumerable<Member>> GetMembers()
        {
            var members = await _memberDao.GetMembers().ConfigureAwait(false);
            var memberTransfoTasks = members.Select(TransformToMemberEntity).ToArray();
            return await Task.WhenAll(memberTransfoTasks).ConfigureAwait(false);
        }

        private async Task<Member> TransformToMemberEntity(MemberDto memberDto)
        {
            if (memberDto != null)
            {
                return new Member
                {
                    Id = memberDto.Id,
                    Email = memberDto.Email,
                    FirstName = memberDto.FirstName,
                    LastName = memberDto.LastName,
                    Role = (CommunityRole)memberDto.Role,
                    Artifacts = await _artifactRepository.GetMemberArtifacts(memberDto.Id).ConfigureAwait(false)
                };
            }
            return null;
        }

        public async Task<Member> GetMember(int id)
        {
            return await TransformToMemberEntity(await _memberDao.GetMember(id).ConfigureAwait(false)).ConfigureAwait(false);
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
            return await TransformToMemberEntity(await _memberDao.GetMember(email).ConfigureAwait(false)).ConfigureAwait(false);
        }

        public async Task UpdateMember(Member member)
        {
            var memberDto = TransformToMemberDto(member);
            await _memberDao.UpdateMember(memberDto).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Member>> GetCommunityMembers(int id)
        {
            var communityMembers = await _memberDao.GetCommunityMembers(id).ConfigureAwait(false);
            var communityMembersTransforTasks =  communityMembers.Select(TransformToMemberEntity).ToArray();
            return await Task.WhenAll(communityMembersTransforTasks).ConfigureAwait(false);
        }
    }
}
