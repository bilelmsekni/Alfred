using System.Collections.Generic;
using Alfred.Dal.Standard.Entities.Artifacts;
using Alfred.Dal.Standard.Entities.Base;
using Alfred.Dal.Standard.Entities.Communities;
using Alfred.Dal.Standard.Entities.Members;
using Alfred.Standard.Models.Artifacts;
using Alfred.Standard.Models.Base;
using Alfred.Standard.Models.Communities;
using Alfred.Standard.Models.Members;

namespace Alfred.Dal.Standard.Mappers
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
        ArtifactCriteria CreateArtifactCrtieria(ArtifactCriteriaModel criteriaModel);
        MemberCriteria CreateMemberCriteria(MemberCriteriaModel criteriaModel);
        CommunityCriteria CreateCommunityCriteria(CommunityCriteriaModel criteriaModel);
        ArtifactResponseModel CreateArtifactResponseModel(ArtifactResponse artifactResponse);
        CommunityResponseModel CreateCommunityResponseModel(CommunityResponse communityResponse);
        LinkModel CreateLinkModel(Link link, Dictionary<string, object> queryParams);
        MemberResponseModel CreateMemberResponseModel(MemberResponse memberResponse);
    }
}