using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        public IEnumerable<Member> GetMembers()
        {
            return _memberDao.GetMembers().Select(TransformToMemberEntity);
        }

        private Member TransformToMemberEntity(MemberDto memberDto)
        {
            return new Member
            {
                Id = memberDto.Id,
                Email = memberDto.Email,
                FirstName = memberDto.FirstName,
                LastName = memberDto.LastName,
                Role = (CommunityRole) memberDto.Role,
                Artifacts = _artifactRepository.GetMemberArtifacts(memberDto.Id)                
            };
        }

        public Member GetMember(int id)
        {
            return TransformToMemberEntity(_memberDao.GetMember(id));
        }

        public void SaveMember(Member member)
        {
            var memberDto = TransformToMemberDto(member);
            _memberDao.SaveMember(memberDto);
        }

        private MemberDto TransformToMemberDto(Member member)
        {
            return new MemberDto
            {
                Id = member.Id,
                Email = member.Email,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Role = (int) member.Role
            };
        }

        public void DeleteMember(int id)
        {
            _memberDao.DeleteMember(id);
        }

        public Member GetMember(string email)
        {
            return TransformToMemberEntity(_memberDao.GetMember(email));
        }

        public void UpdateMember(Member member)
        {
            var memberDto = TransformToMemberDto(member);
            _memberDao.UpdateMember(memberDto);
        }

        public IEnumerable<Member> GetCommunityMembers(int id)
        {
            return _memberDao.GetCommunityMembers(id).Select(TransformToMemberEntity);
        }
    }
}
