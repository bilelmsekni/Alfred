using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<MemberModel> GetMembers()
        {
            var memberEntities = _memberRepository.GetMembers();
            return memberEntities.Select(x => _modelFactory.CreateMemberModel(x));
        }

        public MemberModel GetMember(int id)
        {
            var memberEntity = _memberRepository.GetMember(id);
            return _modelFactory.CreateMemberModel(memberEntity);
        }

        public int CreateMember(CreateMemberModel createMemberModel)
        {
            var member = _modelFactory.CreateMember(createMemberModel);
            if (member != null && !IsEmailUsed(member.Email))
            {
                return _memberRepository.SaveMember(member);
            }
            return -1;
        }

        public bool DeleteMember(int id)
        {
            var member = _memberRepository.GetMember(id);
            if (member != null)
            {
                _memberRepository.DeleteMember(id);
                return true;
            }
            return false;
        }

        public MemberModel UpdateMember(UpdateMemberModel updateMemberModel)
        {
            var originalMember = _memberRepository.GetMember(updateMemberModel.Id);
            if (originalMember != null)
            {
                var member = _modelFactory.CreateMember(updateMemberModel, originalMember);
                if (member != null)
                {
                    _memberRepository.UpdateMember(member);
                    return _modelFactory.CreateMemberModel(member);
                }
            }
            return null;
        }

        private bool IsEmailUsed(string email)
        {
            return _memberRepository.GetMember(email) != null;
        }
    }
}