using System.Collections.Generic;
using Alfred.Dal.Implementation.Fake.Standard.EntityDtos;
using Alfred.Dal.Standard.Entities.Artifacts;
using Alfred.Dal.Standard.Entities.Communities;
using Alfred.Dal.Standard.Entities.Members;

namespace Alfred.Dal.Implementation.Fake.Standard.Mappers
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