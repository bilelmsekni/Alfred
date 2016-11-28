using Alfred.Dal.Entities.Artifact;
using Alfred.Dal.Entities.Community;
using Alfred.Dal.Entities.Member;
using Alfred.Dal.Implementation.Fake.EntityDtos;

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