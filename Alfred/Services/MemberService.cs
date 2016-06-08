using System.Collections.Generic;
using Alfred.Dal.Entities.Member;
using Alfred.Dal.Interfaces;
using Alfred.Model;
using Alfred.Model.Members;

namespace Alfred.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IModelFactory _modelFactory;

        public MemberService(IMemberRepository memberRepository, IModelFactory modelFactory)
        {
            _memberRepository = memberRepository;
            _modelFactory = modelFactory;
        }
        public IEnumerable<Member> GetMembers()
        {
            return _memberRepository.GetMembers();
        }

        public Member GetMember(int id)
        {
            return _memberRepository.GetMember(id);
        }

        public MemberModel CreateMember(CreateMemberModel createMemberModel)
        {
            var model = _modelFactory.CreateModel(createMemberModel);
            return null;
        }
    }
}