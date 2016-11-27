using Alfred.Dal.Implementation.Fake.EntityDtos;
using Alfred.Domain.Entities.Artifact;
using Alfred.Domain.Entities.Community;
using Alfred.Domain.Entities.Member;
using Alfred.Shared.Enums;

namespace Alfred.Dal.Implementation.Fake.Mappers
{    
    public class EntityFactory : IEntityFactory
    {
        public ArtifactDto TransformToArtifactDto(Artifact artifact)
        {
            if (artifact != null)
            {
                return new ArtifactDto
                {
                    Id = artifact.Id,
                    Title = artifact.Title,
                    Bonus = artifact.Bonus,
                    Reward = artifact.Reward,
                    Status = (int)artifact.Status,
                    Type = (int)artifact.Type,
                    MemberId = artifact.MemberId,
                    CommunityId = artifact.CommunityId
                };
            }
            return null;
        }


        public Artifact TransformToArtifactEntity(ArtifactDto artifactDto)
        {
            if (artifactDto == null) return null;

            return new Artifact
            {
                Id = artifactDto.Id,
                Title = artifactDto.Title,
                Bonus = artifactDto.Bonus,
                Reward = artifactDto.Reward,
                Status = (ArtifactStatus)artifactDto.Status,
                Type = (ArtifactType)artifactDto.Type,
                MemberId = artifactDto.MemberId,
                CommunityId = artifactDto.CommunityId
            };
        }

        public Community TransformToCommunityEntity(CommunityDto communityDto)
        {
            if (communityDto != null)
            {
                return new Community
                {
                    Id = communityDto.Id,
                    Email = communityDto.Email,
                    Name = communityDto.Name
                };
            }
            return null;
        }

        public CommunityDto TransformToCommunityDto(Community community)
        {
            if (community != null)
            {
                return new CommunityDto
                {
                    Id = community.Id,
                    Email = community.Email,
                    Name = community.Name
                };
            }
            return null;
        }

        public Member TransformToMemberEntity(MemberDto memberDto)
        {
            if (memberDto != null)
            {
                return new Member
                {
                    Id = memberDto.Id,
                    Email = memberDto.Email,
                    FirstName = memberDto.FirstName,
                    LastName = memberDto.LastName,
                    Role = (CommunityRole)memberDto.Role,
                    CommunityIds = memberDto.CommunityIds                    
                };
            }
            return null;
        }

        public MemberDto TransformToMemberDto(Member member)
        {
            return new MemberDto
            {
                Id = member.Id,
                Email = member.Email,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Role = (int)member.Role
            };
        }
    }
}
