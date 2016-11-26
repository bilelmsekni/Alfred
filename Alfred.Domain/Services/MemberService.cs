using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Domain.Repositories;
using Alfred.Models.Members;
using Alfred.Services;

namespace Alfred.Domain.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public async Task<IEnumerable<MemberModel>> GetMembers()
        {
            return await _memberRepository.GetMembers().ConfigureAwait(false);
            //return memberEntities.Select(x => _modelFactory.CreateMemberModel(x));
        }

        public async Task<MemberModel> GetMember(int id)
        {
            return await _memberRepository.GetMember(id).ConfigureAwait(false);
            //return _modelFactory.CreateMemberModel(memberEntity);
        }

        public async Task<int> CreateMember(CreateMemberModel createMemberModel)
        {
            if (createMemberModel != null)
            {
                return await _memberRepository.SaveMember(createMemberModel).ConfigureAwait(false);
            }
            //var member = _modelFactory.CreateMember(createMemberModel);
            //if (member != null && !IsEmailUsed(member.Email))
            //{
            //    return await _memberRepository.SaveMember(member).ConfigureAwait(false);
            //}
            return -1;
        }

        public async Task<bool> DeleteMember(int id)
        {
            var member = await _memberRepository.GetMember(id).ConfigureAwait(false);
            if (member != null)
            {
                await _memberRepository.DeleteMember(id).ConfigureAwait(false);
                return true;
            }
            return false;
        }

        public async Task<MemberModel> UpdateMember(UpdateMemberModel updateMemberModel)
        {
            if (updateMemberModel != null)
            {
                    await _memberRepository.UpdateMember(updateMemberModel).ConfigureAwait(false);

            }
            //var originalMember = await _memberRepository.GetMember(updateMemberModel.Id).ConfigureAwait(false);
            //if (originalMember != null)
            //{
            //    var member = _modelFactory.CreateMember(updateMemberModel, originalMember);
            //    if (member != null)
            //    {
            //        await _memberRepository.UpdateMember(member).ConfigureAwait(false);
            //        return _modelFactory.CreateMemberModel(member);
            //    }
            //}
            return null;
        }

        private bool IsEmailUsed(string email)
        {
            return _memberRepository.GetMember(email) != null;
        }
    }
}