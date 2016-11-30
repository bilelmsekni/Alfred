using System;
using System.Collections.Generic;
using Alfred.Dal.Entities.Artifact;
using Alfred.Dal.Entities.Base;
using Alfred.Dal.Entities.Community;
using Alfred.Dal.Entities.Member;
using Alfred.Models.Artifacts;
using Alfred.Models.Base;
using Alfred.Models.Communities;
using Alfred.Models.Members;

namespace Alfred.Dal.Mappers
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