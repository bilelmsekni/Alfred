using Alfred.Dal.Implementation.Fake.EntityDtos;
using Alfred.Domain.Entities.Artifact;
using Alfred.Domain.Entities.Community;
using Alfred.Domain.Entities.Member;

namespace Alfred.Dal.Implementation.Fake.Mappers
{
    public interface IEntityFactory
    {
        ArtifactDto TransformToArtifactDto(Artifact artifact);
        Artifact TransformToArtifactEntity(ArtifactDto artifactDto);
        Community TransformToCommunityEntity(CommunityDto communityDto);
        CommunityDto TransformToCommunityDto(Community community);
        Member TransformToMemberEntity(MemberDto memberDto);
        MemberDto TransformToMemberDto(Member member);
    }
}