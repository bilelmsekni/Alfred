using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Alfred.Dal.Entities.Community;
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
            return await Task.Run(() => _memberDao.GetMembers().Select(TransformToMemberEntity));
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
                    Role = (CommunityRole)memberDto.Role,
                    Artifacts = _artifactRepository.GetMemberArtifacts(memberDto.Id)
                };
            }
            return null;
        }

        public async Task<Member> GetMember(int id)
        {
            return TransformToMemberEntity(await Task.Run(() => _memberDao.GetMember(id)));
        }

        public async Task<int> SaveMember(Member member)
        {
            var memberDto = TransformToMemberDto(member);
            return await Task.Run(() => _memberDao.SaveMember(memberDto));
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

        public void DeleteMember(int id)
        {
            _memberDao.DeleteMember(id);
        }

        public async Task<Member> GetMember(string email)
        {
            return TransformToMemberEntity(await Task.Run(() => _memberDao.GetMember(email)));
        }

        public void UpdateMember(Member member)
        {
            var memberDto = TransformToMemberDto(member);
            _memberDao.UpdateMember(memberDto);
        }

        public async Task<IEnumerable<Member>> GetCommunityMembers(int id)
        {
            return await Task.Run(() => _memberDao.GetCommunityMembers(id).Select(TransformToMemberEntity));
        }
    }
}
