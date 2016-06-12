using Alfred.Dal.Entities.Community;
using Alfred.Dal.Entities.Member;
using Alfred.Model.Communities;
using Alfred.Model.Members;

namespace Alfred.Model
{
    public interface IModelFactory
    {
        Member CreateMember(CreateMemberModel createMemberModel);
        MemberModel CreateMemberModel(Member member);
        Member CreateMember(UpdateMemberModel updateMemberModel);
        CommunityModel CreateCommunityModel(Community community);
    }
}
