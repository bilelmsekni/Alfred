using System.Collections.Generic;
using Alfred.Dal.Entities;
using Alfred.Dal.Interfaces;

namespace Alfred.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;

        }
        public IEnumerable<Member> GetMembers()
        {
            return _memberRepository.GetMembers();
        }

        public Member GetMember(int id)
        {
            return _memberRepository.GetMember(id);
        }
    }
}