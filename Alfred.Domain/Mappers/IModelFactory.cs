using Alfred.Domain.Entities.Artifact;
using Alfred.Domain.Entities.Community;
using Alfred.Domain.Entities.Member;
using Alfred.Models.Artifacts;
using Alfred.Models.Communities;
using Alfred.Models.Members;

namespace Alfred.Domain.Mappers
{
    public interface IModelFactory
    {
        Member CreateMember(CreateMemberModel createMemberModel);
        MemberModel CreateMemberModel(Member member);
        Member CreateMember(UpdateMemberModel updateMemberModel, Member originalMember);
        CommunityModel CreateCommunityModel(Community community);
        Community CreateCommunity(CreateCommunityModel createCommunityModel);
        Community CreateCommunity(UpdateCommunityModel updateCommunityModel, Community originalCommunity);
        ArtifactModel CreateArtifactModel(Artifact artifact);
        Artifact CreateArtifact(CreateArtifactModel createArtifactModel);
        Artifact CreateArtifact(UpdateArtifactModel updateArtifactModel, Artifact oldArtifactModel);
    }
}