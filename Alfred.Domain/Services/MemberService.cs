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
        }

        public async Task<MemberModel> GetMember(int id)
        {
            return await _memberRepository.GetMember(id).ConfigureAwait(false);
        }

        public async Task<int> CreateMember(CreateMemberModel createMemberModel)
        {
            if (createMemberModel != null)
            {
                return await _memberRepository.SaveMember(createMemberModel).ConfigureAwait(false);
            }           
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
            return null;
        }
    }
}