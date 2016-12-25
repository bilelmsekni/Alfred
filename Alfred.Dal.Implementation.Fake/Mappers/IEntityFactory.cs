using System.Collections.Generic;
using Alfred.Dal.Entities.Artifacts;
using Alfred.Dal.Entities.Communities;
using Alfred.Dal.Entities.Members;
using Alfred.Dal.Implementation.Fake.EntityDtos;

namespace Alfred.Dal.Implementation.Fake.Mappers
{
    public interface IEntityFactory
    {
        ArtifactDto TransformToArtifactDto(Artifact artifact);
        Artifact TransformToArtifactEntity(ArtifactDto artifactDto);
        Community TransformToCommunityEntity(CommunityDto communityDto);
        CommunityDto TransformToCommunityDto(Community community);
        Member TransformToMemberEntity(MemberDto memberDto, IEnumerable<CommunityDto> communities);
        MemberDto TransformToMemberDto(Member member);
    }
}