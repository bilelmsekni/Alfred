using Alfred.Dal.Entities.Artifact;
using Alfred.Dal.Entities.Community;
using Alfred.Dal.Entities.Member;
using Alfred.Model.Artifacts;
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
        Community CreateCommunity(CreateCommunityModel createCommunityModel);
        Community CreateCommunity(UpdateCommunityModel updateCommunityModel);
        ArtifactModel CreateArtifactModel(Artifact artifact);
        Artifact CreateArtifact(CreateArtifactModel createArtifactModel);
        Artifact CreateArtifact(UpdateArtifactModel updateArtifactModel, Artifact oldArtifactModel);
    }
}